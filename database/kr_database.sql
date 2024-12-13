--
-- PostgreSQL database dump
--

-- Dumped from database version 16.4
-- Dumped by pg_dump version 16.4

-- Started on 2024-12-13 16:05:30

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 254 (class 1255 OID 32991)
-- Name: analyze_ward_occupancy(double precision, double precision); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.analyze_ward_occupancy(IN lower_bound double precision, IN upper_bound double precision)
    LANGUAGE plpgsql
    AS $$
DECLARE
    diagnosis_record RECORD;
    occupancy_ratio FLOAT;
    total_occupied INT;
    total_free INT;
	rec1 RECORD;
	-- Объявление курсора для перебора диагнозов и их заполненности
    DECLARE diagnosis_cursor CURSOR FOR
		SELECT 
			d.id AS diagnosis_id,
			d.name AS diagnosis_name,
			SUM(p.total_patients) AS total_patients,          -- Подсчитываем общее количество пациентов
			SUM(w.max_count) AS total_capacity,                             -- Суммируем максимальную вместимость палат
			SUM(p.total_patients)::FLOAT / NULLIF(SUM(w.max_count), 0) AS occupancy_ratio  -- Рассчитываем отношение занятого места к свободному
		FROM diagnosis d
		JOIN wards w ON d.id = w.diagnosis_id
		LEFT JOIN 
			(SELECT ward_id, COUNT(*) AS total_patients 
			 FROM people 
			 GROUP BY ward_id) p ON p.ward_id = w.id
		GROUP BY d.id, d.name
		HAVING SUM(w.max_count) > 0 AND -- Убедимся, что есть свободные места
						(SUM(w.max_count) - SUM(p.total_patients)) > 0 AND -- Проверка на попадание
			SUM(p.total_patients)::FLOAT / 
			SUM(w.max_count) BETWEEN lower_bound AND upper_bound;  -- в заданный интервал заполненности
       
BEGIN
    -- Создаем временную таблицу для хранения результатов
    CREATE TEMP TABLE temp_results (
        diagnosis_id INT,
       -- diagnosis_name VARCHAR,
        average_occupancy FLOAT
    );

    -- Открываем курсор
    OPEN diagnosis_cursor;

    FETCH diagnosis_cursor INTO diagnosis_record;

    WHILE FOUND LOOP
        -- Считаем общее количество занятых и свободных мест для данного диагноза
        total_occupied := diagnosis_record.total_patients;
        total_free := diagnosis_record.total_capacity - total_occupied;

        -- Вычисляем отношение и проверяем, попадает ли оно в заданный диапазон
        IF total_free > 0 THEN
            occupancy_ratio := total_occupied::FLOAT / total_free;
			--RAISE NOTICE 'diagnosis_record.diagnosis_id: %', diagnosis_record.diagnosis_id;
			--RAISE NOTICE 'total_occupied: %', total_occupied;
			--RAISE NOTICE 'total_free: %', total_free;
            --IF occupancy_ratio BETWEEN lower_bound AND upper_bound THEN
                -- Считаем среднюю заполненность и сохраняем результат
                INSERT INTO temp_results (diagnosis_id, average_occupancy)
                VALUES (diagnosis_record.diagnosis_id, occupancy_ratio);
          --  END IF;
        END IF;

        -- Извлекаем следующую запись из курсора
        FETCH diagnosis_cursor INTO diagnosis_record;
    END LOOP;

    -- Закрываем курсор
    CLOSE diagnosis_cursor;

	-- Выводим результаты
	FOR rec1 IN
		SELECT diagnosis_id, average_occupancy
		FROM temp_results		
    LOOP
        RAISE NOTICE 'diagnosis_id: %, average_occupancy: %', rec1.diagnosis_id, rec1.average_occupancy;
    END LOOP;
 
    -- Удаляем временную таблицу
    DROP TABLE temp_results;
END;
$$;


ALTER PROCEDURE public.analyze_ward_occupancy(IN lower_bound double precision, IN upper_bound double precision) OWNER TO postgres;

--
-- TOC entry 248 (class 1255 OID 32921)
-- Name: available_wards(integer, integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.available_wards(IN diagnosis_1 integer, IN diagnosis_2 integer)
    LANGUAGE plpgsql
    AS $$
DECLARE
    people_diagnosis_count real;
    ward record; -- Переменная записи в таблице wards
BEGIN
   -- Вычисление среднего количества людей в палатах с заданными диагнозами
    SELECT AVG(real_count) INTO people_diagnosis_count
    FROM (
        SELECT wards.id, wards.name, count(people.last_name) AS real_count
        FROM wards
        JOIN people ON people.ward_id = wards.id
        WHERE wards.diagnosis_id IN (diagnosis_1, diagnosis_2)
        GROUP BY wards.id
    ) AS subquery;

	RAISE NOTICE 'Id и названия определённых палат:';
    -- Поиск палат, где занятых мест меньше, чем people_diagnosis_count
    FOR ward IN 
        SELECT wards.id, wards.name
        FROM wards
        LEFT JOIN people ON people.ward_id = wards.id
        GROUP BY wards.id
        HAVING count(people.last_name) < people_diagnosis_count
    LOOP
        RAISE NOTICE 'Ward ID: %, Ward Name: %', ward.id, ward.name;
    END LOOP;
END;
$$;


ALTER PROCEDURE public.available_wards(IN diagnosis_1 integer, IN diagnosis_2 integer) OWNER TO postgres;

--
-- TOC entry 251 (class 1255 OID 33403)
-- Name: check_diagnosis_data(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.check_diagnosis_data() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Проверка на пустое значение поля name
    IF NEW.name IS NULL OR NEW.name = '' THEN
        RAISE EXCEPTION 'Поля записи не могут быть нулевыми';
    END IF;

    RETURN NEW;
END;
$$;


ALTER FUNCTION public.check_diagnosis_data() OWNER TO postgres;

--
-- TOC entry 244 (class 1255 OID 32974)
-- Name: check_diagnosis_ref(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.check_diagnosis_ref() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    people_count_d INT;
	wards_count_d INT;
	current_name VARCHAR;
BEGIN
	-- Считаем количество ссылок на диагноз с нужным id в таблице people
	SELECT COUNT(*) INTO people_count_d
    FROM people WHERE diagnosis_id = NEW.id;
	-- Считаем количество ссылок на диагноз с нужным id в таблице wards
	SELECT COUNT(*) INTO wards_count_d
    FROM wards WHERE diagnosis_id = NEW.id;
	-- Если ссылки есть (их количество хотя бы в одной из таблиц больше нуля) и имя изменилось
	IF (people_count_d > 0 OR wards_count_d > 0) THEN
        RAISE EXCEPTION 'Нельзя изменить наименование диагноза, т. к. на него есть ссылки';
    END IF;
    RETURN NEW;
END;
$$;


ALTER FUNCTION public.check_diagnosis_ref() OWNER TO postgres;

--
-- TOC entry 260 (class 1255 OID 33390)
-- Name: check_patient_diagnosis(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.check_patient_diagnosis() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Проверка, есть ли палата с таким ward_id
    IF NOT EXISTS (
        SELECT 1
        FROM wards
        WHERE id = NEW.ward_id
          AND diagnosis_id = NEW.diagnosis_id
    ) THEN
        -- Если палата с указанным ward_id и diagnosis_id не найдена, выбрасываем исключение
        RAISE EXCEPTION 'Диагноз % пациента не соответствует диагнозу палаты %',
            NEW.diagnosis_id, NEW.ward_id;
    END IF;

    -- Если проверка пройдена, разрешаем операцию
    RETURN NEW;
END;
$$;


ALTER FUNCTION public.check_patient_diagnosis() OWNER TO postgres;

--
-- TOC entry 253 (class 1255 OID 33399)
-- Name: check_people_data(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.check_people_data() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Проверка на пустые значения
    IF NEW.first_name IS NULL OR NEW.last_name IS NULL OR NEW.father_name IS NULL
	OR NEW.ward_id IS NULL OR NEW.diagnosis_id IS NULL THEN
        RAISE EXCEPTION 'Поля записи не могут быть нулевыми';
    END IF;

    -- Проверка на наличие цифр в ФИО
    IF NEW.first_name ~ '\d' OR NEW.last_name ~ '\d' OR NEW.father_name ~ '\d' THEN
        RAISE EXCEPTION 'Фамилия, имя и отчество должны состоять только из букв';
    END IF;

    -- Проверка ward_id и diagnosis_id на положительные числа
    IF NEW.ward_id <= 0 THEN
        RAISE EXCEPTION 'Номер палаты должен быть положительным числом';
    END IF;

    IF NEW.diagnosis_id <= 0 THEN
        RAISE EXCEPTION 'Код диагноза должен быть положительным числом';
    END IF;

    RETURN NEW;
END;
$$;


ALTER FUNCTION public.check_people_data() OWNER TO postgres;

--
-- TOC entry 249 (class 1255 OID 33405)
-- Name: check_people_references(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.check_people_references() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Проверка на существование палаты
    IF NOT EXISTS (SELECT 1 FROM wards WHERE id = NEW.ward_id) THEN
        RAISE EXCEPTION 'Палата с номером % не существует', NEW.ward_id;
    END IF;

    -- Проверка на существование диагноза
    IF NOT EXISTS (SELECT 1 FROM diagnosis WHERE id = NEW.diagnosis_id) THEN
        RAISE EXCEPTION 'Диагноз с кодом % не существует', NEW.diagnosis_id;
    END IF;

    RETURN NEW;
END;
$$;


ALTER FUNCTION public.check_people_references() OWNER TO postgres;

--
-- TOC entry 228 (class 1255 OID 32950)
-- Name: check_ward_capacity(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.check_ward_capacity() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    current_count INT;
BEGIN
    -- Получаем текущее количество пациентов в палате
    SELECT COUNT(*) INTO current_count
    FROM people
    WHERE ward_id = NEW.ward_id;

    -- Проверяем, не превышает ли текущее количество максимальную вместимость
    IF current_count >= (SELECT max_count FROM wards WHERE id = NEW.ward_id) THEN
        RAISE EXCEPTION 'Нет свободного места в палате с ID %', NEW.ward_id;
    END IF;

    RETURN NEW;
END;
$$;


ALTER FUNCTION public.check_ward_capacity() OWNER TO postgres;

--
-- TOC entry 264 (class 1255 OID 33401)
-- Name: check_wards_data(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.check_wards_data() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Проверка на пустое значение поля name
    IF NEW.name IS NULL OR NEW.name = '' THEN
        RAISE EXCEPTION 'Поля записи не могут быть нулевыми';
    END IF;
	
	
   /* IF NEW.max_count IS NULL OR NEW.max_count = '' THEN
        RAISE EXCEPTION 'Поля записи не могут быть нулевыми';
    END IF;*/
	
	
    /*IF NEW.diagnosis_id IS NULL OR NEW.diagnosis_id = '' THEN
        RAISE EXCEPTION 'Поля записи не могут быть нулевыми';
    END IF;*/

	-- Проверка на пустое значение поля max_count
    -- Проверка max_count на положительное целое значение
    IF NEW.max_count IS NULL OR NEW.max_count = '' /*OR NEW.max_count <= 0*/ THEN
        RAISE EXCEPTION 'Вместимость палаты должна быть положительным числом';
    END IF;

	-- Проверка на пустое значение поля diagnosis_id
    -- Проверка diagnosis_id на положительное значение
    IF NEW.diagnosis_id IS NULL OR NEW.diagnosis_id = '' /*OR NEW.diagnosis_id <= 0*/ THEN
        RAISE EXCEPTION 'Код диагноза должен быть положительным числом';
    END IF;

    RETURN NEW;
END;
$$;


ALTER FUNCTION public.check_wards_data() OWNER TO postgres;

--
-- TOC entry 250 (class 1255 OID 33407)
-- Name: check_wards_references(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.check_wards_references() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Проверка на существование диагноза
    IF NOT EXISTS (SELECT 1 FROM diagnosis WHERE id = NEW.diagnosis_id) THEN
        RAISE EXCEPTION 'Диагноз с кодом % не существует', NEW.diagnosis_id;
    END IF;

    RETURN NEW;
END;
$$;


ALTER FUNCTION public.check_wards_references() OWNER TO postgres;

--
-- TOC entry 256 (class 1255 OID 33100)
-- Name: get_sorted_wards(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_sorted_wards() RETURNS TABLE(id integer, name character varying, max_count integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT w.id, w.name, w.max_count
    FROM public.wards AS w
    ORDER BY w.max_count DESC, w.name ASC;
END;
$$;


ALTER FUNCTION public.get_sorted_wards() OWNER TO postgres;

--
-- TOC entry 255 (class 1255 OID 33096)
-- Name: get_sorted_wards_proc(); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.get_sorted_wards_proc()
    LANGUAGE sql
    AS $$
SELECT id, name, max_count
FROM public.wards
ORDER BY max_count DESC, name ASC;
$$;


ALTER PROCEDURE public.get_sorted_wards_proc() OWNER TO postgres;

--
-- TOC entry 252 (class 1255 OID 32963)
-- Name: get_ward_ratios(integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.get_ward_ratios(IN ward_1 integer, OUT same_diagnosis_ward_id integer, OUT lowest_other_diagnosis_ratio double precision)
    LANGUAGE plpgsql
    AS $$
DECLARE
    current_id INT;
	current_name VARCHAR;
	current_diagnosis_id INT;
	new_current_diagnosis_id INT;
    current_max_count INT;
    current_occupied_count INT;
    current_ratio FLOAT;
	ward record;
BEGIN
    -- Получаем диагноз и максимальную вместимость текущей палаты
    SELECT diagnosis_id, max_count INTO current_diagnosis_id, current_max_count
    FROM wards
    WHERE id = ward_1;

    -- Получаем количество занятых мест в текущей палате
    SELECT COUNT(*) INTO current_occupied_count
    FROM people
    WHERE ward_id = ward_1;
	
    -- Проверяем, есть ли свободные места в текущей палате
    IF current_max_count - current_occupied_count > 0 THEN
        current_ratio := current_occupied_count::FLOAT / (current_max_count - current_occupied_count);
    ELSE
		current_ratio := 1000000;
    END IF;

	-- Находим палату с тем же диагнозом и самым низким отношением
	FOR ward IN 
        SELECT wards.id, wards.name, wards.max_count, wards.diagnosis_id
        FROM wards
		WHERE wards.diagnosis_id = current_diagnosis_id /*AND wards.id <> ward_1*/
	LOOP
		SELECT COUNT(*) INTO current_occupied_count
		FROM people
		WHERE ward_id = ward.id;
		current_max_count := ward.max_count;
		IF current_max_count - current_occupied_count > 0 THEN		
			IF current_occupied_count::FLOAT / 
							(current_max_count - current_occupied_count) <= current_ratio THEN
								current_ratio := current_occupied_count::FLOAT / 
											(current_max_count - current_occupied_count);
								current_id := ward.id;
			END IF;
		ELSE
			CONTINUE;
		END IF;
	IF current_ratio = 1000000 THEN
		RAISE NOTICE 'Во всех палатах в данном случае все места заняты, поэтому при вычислении отношения происходит деление на нуль, и все отношения можно считать одинаковыми';
	ELSE
		SELECT wards.diagnosis_id, wards.name, wards.max_count INTO 
			   new_current_diagnosis_id, current_name, current_max_count
			   FROM wards WHERE wards.id = current_id;
		same_diagnosis_ward_id := current_id;
		--RAISE NOTICE 'Палата с таким же диагнозом и самым низким отношением занятого места к свободному:';
        --RAISE NOTICE 'Ward ID: %, Ward Name: %, Ward Max_count: %, Ward Diagnosis_id: % ',
					-- current_id, current_name, current_max_count, new_current_diagnosis_id;
    END IF;
	END LOOP;


    -- Находим минимальное отношение среди палат с другими диагнозами	
	FOR ward IN 
        SELECT wards.id, wards.name, wards.max_count, wards.diagnosis_id
        FROM wards
		WHERE wards.diagnosis_id <> current_diagnosis_id
	LOOP
		SELECT COUNT(*) INTO current_occupied_count
		FROM people
		WHERE ward_id = ward.id;
		current_max_count := ward.max_count;
		IF current_max_count - current_occupied_count > 0 THEN		
			IF current_occupied_count::FLOAT / 
							(current_max_count - current_occupied_count) <= current_ratio THEN
								current_ratio := current_occupied_count::FLOAT / 
											(current_max_count - current_occupied_count);
			END IF;
		ELSE
			CONTINUE;
		END IF;
	IF current_ratio = 1000000 THEN
		RAISE NOTICE 'Во всех палатах в данном случае все места заняты, поэтому при вычислении отношения происходит деление на нуль, и все отношения можно считать одинаковыми';
	ELSE
		lowest_other_diagnosis_ratio := current_ratio;
		--RAISE NOTICE 'Самое низкое отношение занятого места к свободному
					-- среди палат других диагнозов: % ', current_ratio;
    END IF;
	END LOOP;
END;
$$;


ALTER PROCEDURE public.get_ward_ratios(IN ward_1 integer, OUT same_diagnosis_ward_id integer, OUT lowest_other_diagnosis_ratio double precision) OWNER TO postgres;

--
-- TOC entry 241 (class 1255 OID 32953)
-- Name: get_wards_with_patients(); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.get_wards_with_patients()
    LANGUAGE plpgsql
    AS $$
DECLARE
rec1 record;
BEGIN
    -- Выполняем SELECT для получения палатов и количества пациентов
    RAISE NOTICE 'Палаты с пациентами:';
    
    FOR rec1 IN
		SELECT wards.id wards_id , wards.name wards_name,
		   count(people.last_name) AS real_patient_count			  				
		FROM wards
		JOIN people ON people.ward_id = wards.id
		GROUP BY wards.id
		HAVING COUNT(people.id) > 0
    LOOP
        RAISE NOTICE 'Палата ID: %, Название: %, Количество пациентов: %', rec1.wards_id, rec1.wards_name, rec1.real_patient_count;
    END LOOP;
END;
$$;


ALTER PROCEDURE public.get_wards_with_patients() OWNER TO postgres;

--
-- TOC entry 263 (class 1255 OID 33413)
-- Name: handle_ward_capacity_change(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.handle_ward_capacity_change() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    total_free_spaces INT; -- Количество свободных мест для данного диагноза
    excess_count INT; -- Количество излишних пациентов
    excess_patients RECORD;
    available_ward RECORD;
BEGIN
    -- Считаем количество излишних пациентов
    excess_count := (SELECT COUNT(*) FROM people WHERE ward_id = OLD.id) - NEW.max_count;

    -- Если излишков нет, продолжаем
    IF excess_count > 0 THEN
        -- Считаем общее количество свободных мест в палатах с этим диагнозом
        total_free_spaces := (
            SELECT SUM(max_count - COALESCE((
                SELECT COUNT(*)
                FROM people
                WHERE ward_id = wards.id
            ), 0))
            FROM wards
            WHERE diagnosis_id = OLD.diagnosis_id AND id <> OLD.id
        );

        -- Если свободных мест недостаточно, выбрасываем исключение
        IF total_free_spaces < excess_count THEN
            RAISE EXCEPTION 'Невозможно уменьшить вместимость: недостаточно мест в других палатах для пациентов';
        END IF;

        -- Перемещаем излишних пациентов
        FOR excess_patients IN
            SELECT * FROM people
            WHERE ward_id = OLD.id
            ORDER BY id -- Перемещаем в порядке добавления
            LIMIT excess_count
        LOOP
            -- Ищем первую подходящую палату с достаточным количеством свободных мест
            SELECT * INTO available_ward
            FROM wards
            WHERE diagnosis_id = OLD.diagnosis_id
              AND id <> OLD.id
              AND (SELECT COUNT(*) FROM people WHERE ward_id = wards.id) < max_count
            LIMIT 1;

            -- Перемещаем пациента в найденную палату
            UPDATE people
            SET ward_id = available_ward.id
            WHERE id = excess_patients.id;
        END LOOP;
    END IF;

    -- Обновляем вместимость палаты
    UPDATE wards
    SET max_count = NEW.max_count
    WHERE id = OLD.id;

    RETURN NEW;
END;
$$;


ALTER FUNCTION public.handle_ward_capacity_change() OWNER TO postgres;

--
-- TOC entry 246 (class 1255 OID 33411)
-- Name: handle_ward_diagnosis_change(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.handle_ward_diagnosis_change() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    patient RECORD;
    available_ward RECORD;
BEGIN
    -- Проверяем, есть ли пациенты в палате
    FOR patient IN
        SELECT * FROM people WHERE ward_id = OLD.id
    LOOP
        -- Ищем подходящую палату с тем же диагнозом
        SELECT * INTO available_ward
        FROM wards
        WHERE diagnosis_id = OLD.diagnosis_id
          AND id <> OLD.id
          AND (SELECT COUNT(*) FROM people WHERE ward_id = wards.id) < max_count
        LIMIT 1;

        -- Если подходящей палаты нет, выбрасываем исключение
        IF available_ward IS NULL THEN
            RAISE EXCEPTION 'Невозможно изменить диагноз: нет достаночного количества мест в других палатах для этих пациентов';
        END IF;

        -- Перемещаем пациента в найденную палату
        UPDATE people
        SET ward_id = available_ward.id
        WHERE id = patient.id;
    END LOOP;

    -- Обновляем диагноз палаты
    UPDATE wards
    SET diagnosis_id = NEW.diagnosis_id
    WHERE id = OLD.id;

    RETURN NEW;
END;
$$;


ALTER FUNCTION public.handle_ward_diagnosis_change() OWNER TO postgres;

--
-- TOC entry 242 (class 1255 OID 32912)
-- Name: people_wards(); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.people_wards()
    LANGUAGE plpgsql
    AS $$
declare
    wards_id_ text;
	wards_name_ text;
	real_count_ text;
BEGIN
select array_to_string(array_agg(wards.id), ', ') into wards_id_			  				
	FROM wards
	JOIN people ON people.ward_id = wards.id
	GROUP BY wards.id;
select array_to_string(array_agg(wards.name), ', ') into wards_name_			  				
	FROM wards
	JOIN people ON people.ward_id = wards.id
	GROUP BY wards.id;
/*select array_to_string(array_agg(count(people.last_name)), ', ') into real_count_			  				
	FROM wards
	JOIN people ON people.ward_id = wards.id
	GROUP BY wards.id;*/
	--SELECT wards.id wards_id , wards.name wards_name,
		   --count(people.last_name) AS real_count
	raise notice 'wards_id: %', wards_id_;
	raise notice 'wards_name: %', wards_name_;
	--raise notice 'real_count: %', real_count_;
END;
$$;


ALTER PROCEDURE public.people_wards() OWNER TO postgres;

--
-- TOC entry 227 (class 1255 OID 32947)
-- Name: people_wards(refcursor); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.people_wards(INOUT pp refcursor)
    LANGUAGE plpgsql
    AS $$
BEGIN
OPEN pp FOR
	SELECT wards.id wards_id , wards.name wards_name,
		   count(people.last_name) AS real_count			  				
	FROM wards
	JOIN people ON people.ward_id = wards.id
	GROUP BY wards.id;
	RETURN;
END;
$$;


ALTER PROCEDURE public.people_wards(INOUT pp refcursor) OWNER TO postgres;

--
-- TOC entry 245 (class 1255 OID 33395)
-- Name: prevent_deletion_if_referenced(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.prevent_deletion_if_referenced() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    

    -- Проверяем наличие ссылок на удаляемый диагноз в таблице wards
    IF EXISTS (
        SELECT 1 
        FROM wards 
        WHERE diagnosis_id = OLD.id
    ) THEN
        RAISE EXCEPTION 'Невозможно удалить диагноз, так как он используется в таблице палат (wards)';
    END IF;
	
	-- Проверяем наличие ссылок на удаляемый диагноз в таблице people
    IF EXISTS (
        SELECT 1 
        FROM people 
        WHERE diagnosis_id = OLD.id
    ) THEN
        RAISE EXCEPTION 'Невозможно удалить диагноз, так как он используется в таблице пациентов (people)';
    END IF;

    -- Если зависимых записей нет, продолжаем удаление
    RETURN OLD;
END;
$$;


ALTER FUNCTION public.prevent_deletion_if_referenced() OWNER TO postgres;

--
-- TOC entry 243 (class 1255 OID 33397)
-- Name: prevent_ward_deletion_if_referenced(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.prevent_ward_deletion_if_referenced() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Проверяем наличие ссылок на удаляемую палату в таблице people
    IF EXISTS (
        SELECT 1
        FROM people
        WHERE ward_id = OLD.id
    ) THEN
        RAISE EXCEPTION 'Невозможно удалить палату, так как на неё ссылаются записи в таблице пациентов (people)';
    END IF;

    -- Если зависимых записей нет, продолжаем удаление
    RETURN OLD;
END;
$$;


ALTER FUNCTION public.prevent_ward_deletion_if_referenced() OWNER TO postgres;

--
-- TOC entry 259 (class 1255 OID 33103)
-- Name: show_diagnosis_table(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.show_diagnosis_table() RETURNS TABLE(id integer, name character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT * FROM public.diagnosis;
END;
$$;


ALTER FUNCTION public.show_diagnosis_table() OWNER TO postgres;

--
-- TOC entry 258 (class 1255 OID 33102)
-- Name: show_people_table(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.show_people_table() RETURNS TABLE(id integer, first_name character varying, last_name character varying, father_name character varying, diagnosis_id integer, wadr_id integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT *
    FROM public.people AS p;
END;
$$;


ALTER FUNCTION public.show_people_table() OWNER TO postgres;

--
-- TOC entry 257 (class 1255 OID 33101)
-- Name: show_wards_table(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.show_wards_table() RETURNS TABLE(id integer, name character varying, max_count integer, diagnosis_id integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT w.id, w.name, w.max_count, w.diagnosis_id
    FROM public.wards AS w;
END;
$$;


ALTER FUNCTION public.show_wards_table() OWNER TO postgres;

--
-- TOC entry 261 (class 1255 OID 33393)
-- Name: transfer_patient_to_correct_ward(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transfer_patient_to_correct_ward() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    target_ward_id INT;
BEGIN
    -- Найти подходящую палату для нового диагноза
    SELECT id INTO target_ward_id
    FROM wards
    WHERE diagnosis_id = NEW.diagnosis_id
    LIMIT 1;

    -- Если подходящая палата найдена, обновляем ward_id
    IF target_ward_id IS NOT NULL THEN
        NEW.ward_id = target_ward_id;
    ELSE
        -- Если палата не найдена, выбрасываем исключение
        RAISE EXCEPTION 'Нет подходящей палаты для диагноза %', NEW.diagnosis_id;
    END IF;

    RETURN NEW;
END;
$$;


ALTER FUNCTION public.transfer_patient_to_correct_ward() OWNER TO postgres;

--
-- TOC entry 262 (class 1255 OID 32985)
-- Name: transfer_patients_before_delete(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transfer_patients_before_delete() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    patient RECORD;
    target_ward_id INT;
    available_capacity INT;
BEGIN
    -- Проверяем, есть ли пациенты в удаляемой палате
    FOR patient IN 
        SELECT id, diagnosis_id FROM people WHERE ward_id = OLD.id
    LOOP
        -- Ищем подходящую палату с таким же диагнозом
        SELECT id INTO target_ward_id
        FROM wards
        WHERE diagnosis_id = patient.diagnosis_id
          AND id <> OLD.id
          AND (
              SELECT COUNT(*) FROM people WHERE ward_id = wards.id
          ) < max_count
        LIMIT 1;

        -- Проверяем, нашли ли подходящую палату
        IF target_ward_id IS NULL THEN
            RAISE EXCEPTION 'Невозможно удалить палату: пациент с диагнозом % не может быть перемещён, так как нет подходящих палат.', patient.diagnosis_id;
        END IF;

        -- Перемещаем пациента в подходящую палату
        UPDATE people
        SET ward_id = target_ward_id
        WHERE id = patient.id;
    END LOOP;

    -- Если все пациенты перемещены, удаляем палату
    RETURN OLD;
END;
$$;


ALTER FUNCTION public.transfer_patients_before_delete() OWNER TO postgres;

--
-- TOC entry 247 (class 1255 OID 33409)
-- Name: validate_patient_ward_diagnosis(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.validate_patient_ward_diagnosis() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Проверка существования палаты с указанным диагнозом
    IF NOT EXISTS (
        SELECT 1
        FROM wards
        WHERE id = NEW.ward_id
          AND diagnosis_id = NEW.diagnosis_id
    ) THEN
        RAISE EXCEPTION 'Невозможно добавить пациента в палату %: палата не существует или диагноз не совпадает', NEW.ward_id;
    END IF;

    RETURN NEW;
END;
$$;


ALTER FUNCTION public.validate_patient_ward_diagnosis() OWNER TO postgres;

--
-- TOC entry 229 (class 1255 OID 32949)
-- Name: ward_check(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.ward_check() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
    DECLARE
	people_count NUMERIC;
	places NUMERIC;
	BEGIN
	SELECT COUNT(*) INTO people_count FROM people;
	SELECT SUM(wards.max_count) INTO places FROM wards;
        IF (people_count >= places)
		 THEN
            RAISE EXCEPTION 'Все места в этой палате заняты';
        END IF;
    --RETURN NEW;
    END;
$$;


ALTER FUNCTION public.ward_check() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 219 (class 1259 OID 32827)
-- Name: diagnosis; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.diagnosis (
    id integer NOT NULL,
    name character varying(20)
);


ALTER TABLE public.diagnosis OWNER TO postgres;

--
-- TOC entry 4862 (class 0 OID 0)
-- Dependencies: 219
-- Name: TABLE diagnosis; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public.diagnosis IS 'Таблица диагнозов';


--
-- TOC entry 4863 (class 0 OID 0)
-- Dependencies: 219
-- Name: COLUMN diagnosis.id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.diagnosis.id IS 'Идентификатор диагноза';


--
-- TOC entry 220 (class 1259 OID 32830)
-- Name: diagnosis_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.diagnosis ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.diagnosis_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 221 (class 1259 OID 32831)
-- Name: people; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.people (
    id integer NOT NULL,
    first_name character varying(20),
    last_name character varying(20),
    father_name character varying(20),
    diagnosis_id integer NOT NULL,
    ward_id integer NOT NULL
);


ALTER TABLE public.people OWNER TO postgres;

--
-- TOC entry 4864 (class 0 OID 0)
-- Dependencies: 221
-- Name: TABLE people; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public.people IS 'Таблица больных';


--
-- TOC entry 4865 (class 0 OID 0)
-- Dependencies: 221
-- Name: COLUMN people.id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.people.id IS 'Идентификатор больного';


--
-- TOC entry 4866 (class 0 OID 0)
-- Dependencies: 221
-- Name: COLUMN people.diagnosis_id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.people.diagnosis_id IS 'Идентификатор диагноза';


--
-- TOC entry 4867 (class 0 OID 0)
-- Dependencies: 221
-- Name: COLUMN people.ward_id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.people.ward_id IS 'Идентификатор палаты';


--
-- TOC entry 222 (class 1259 OID 32834)
-- Name: people_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.people ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.people_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 225 (class 1259 OID 32900)
-- Name: v1; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v1 AS
 SELECT id,
    first_name,
    last_name,
    father_name
   FROM public.people;


ALTER VIEW public.v1 OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 32904)
-- Name: v2; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v2 AS
SELECT
    NULL::integer AS wards_id,
    NULL::character varying(20) AS wards_name,
    NULL::integer AS max_count,
    NULL::bigint AS real_count,
    NULL::integer AS diagnosis_id,
    NULL::character varying(20) AS diagnosis_name;


ALTER VIEW public.v2 OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 32835)
-- Name: wards; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.wards (
    id integer NOT NULL,
    name character varying(20),
    max_count integer NOT NULL,
    diagnosis_id integer DEFAULT 0 NOT NULL
);


ALTER TABLE public.wards OWNER TO postgres;

--
-- TOC entry 4868 (class 0 OID 0)
-- Dependencies: 223
-- Name: TABLE wards; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public.wards IS 'Таблица палат';


--
-- TOC entry 4869 (class 0 OID 0)
-- Dependencies: 223
-- Name: COLUMN wards.id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.wards.id IS 'Идентификатор палаты';


--
-- TOC entry 4870 (class 0 OID 0)
-- Dependencies: 223
-- Name: COLUMN wards.name; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.wards.name IS 'Наименование палаты';


--
-- TOC entry 4871 (class 0 OID 0)
-- Dependencies: 223
-- Name: COLUMN wards.max_count; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.wards.max_count IS 'Вместимость палаты (чел)';


--
-- TOC entry 224 (class 1259 OID 32838)
-- Name: wards_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.wards ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.wards_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 4851 (class 0 OID 32827)
-- Dependencies: 219
-- Data for Name: diagnosis; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.diagnosis (id, name) FROM stdin;
3	Артериальная гиперт.
4	Сердечная недостат.
5	Миокардит
6	ПТСР
7	ОКР
8	ОРВИ
9	Грипп
10	Кариес
11	Шизофрения
12	Гастрит
13	Гепатит
21	Остеохондроз
24	Шиз
2	Ишем. болезнь сердца
\.


--
-- TOC entry 4853 (class 0 OID 32831)
-- Dependencies: 221
-- Data for Name: people; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.people (id, first_name, last_name, father_name, diagnosis_id, ward_id) FROM stdin;
262	Смирнова	Лина	Евгеньевна	13	45
278	Анатольва	Анна	Анатольевна	13	45
261	Соколова	Ольга	Афанасьевна	13	45
263	Марков	Марк	Витальевич	13	45
248	Кузнецова	Анна	Игнатьевна	9	2
247	Иванов	Иван	Иванович	12	7
250	Смирнов	Евгений	Петрович	12	7
282	А	Б	В	12	7
254	Смирнова	Лина	Евгеньевна	9	2
255	Павлов	Лев	Глебович	12	7
256	Карпов	Иван	Витальевич	12	7
259	Афанасьев	Афанасий	Афанасьевич	11	4
249	Кузнецов	Александр	Васильевич	4	11
251	Попов	Алексей	Степанович	4	11
252	Новиков	Василий	Андреевич	4	11
260	Козлов	Глеб	Львович	8	1
264	Щербакова	Диана	Даниловна	5	3
257	Иванова	Алла	Андреевна	4	11
274	Андреев	Андрей	Андреевич	11	4
287	А	Б	В	4	11
289	Иванов	Иван	Иванович	8	1
272	Игорев	Игорь	Игоревич	13	45
\.


--
-- TOC entry 4855 (class 0 OID 32835)
-- Dependencies: 223
-- Data for Name: wards; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.wards (id, name, max_count, diagnosis_id) FROM stdin;
2	Обычная (женская) 1	4	9
3	VIP-палата	1	5
4	Интенсивной терапии	3	11
7	Обычная (мужская) 3	5	12
1	Обычная (мужская) 1	4	8
11	Обычная (мужская) 2	6	4
45	Обычная 1	10	13
\.


--
-- TOC entry 4872 (class 0 OID 0)
-- Dependencies: 220
-- Name: diagnosis_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.diagnosis_id_seq', 24, true);


--
-- TOC entry 4873 (class 0 OID 0)
-- Dependencies: 222
-- Name: people_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.people_id_seq', 295, true);


--
-- TOC entry 4874 (class 0 OID 0)
-- Dependencies: 224
-- Name: wards_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.wards_id_seq', 45, true);


--
-- TOC entry 4685 (class 2606 OID 32840)
-- Name: diagnosis diagnosis.id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.diagnosis
    ADD CONSTRAINT "diagnosis.id" PRIMARY KEY (id);


--
-- TOC entry 4687 (class 2606 OID 32842)
-- Name: people people.id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.people
    ADD CONSTRAINT "people.id" PRIMARY KEY (id);


--
-- TOC entry 4689 (class 2606 OID 32844)
-- Name: wards wards.id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.wards
    ADD CONSTRAINT "wards.id" PRIMARY KEY (id);


--
-- TOC entry 4850 (class 2618 OID 32907)
-- Name: v2 _RETURN; Type: RULE; Schema: public; Owner: postgres
--

CREATE OR REPLACE VIEW public.v2 AS
 SELECT wards.id AS wards_id,
    wards.name AS wards_name,
    wards.max_count,
    count(people.last_name) AS real_count,
    diagnosis.id AS diagnosis_id,
    diagnosis.name AS diagnosis_name
   FROM ((public.wards
     JOIN public.diagnosis ON ((wards.diagnosis_id = diagnosis.id)))
     LEFT JOIN public.people ON ((people.ward_id = wards.id)))
  GROUP BY diagnosis.id, wards.id;


--
-- TOC entry 4702 (class 2620 OID 32986)
-- Name: wards before_ward_delete; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER before_ward_delete BEFORE DELETE ON public.wards FOR EACH ROW EXECUTE FUNCTION public.transfer_patients_before_delete();


--
-- TOC entry 4703 (class 2620 OID 33398)
-- Name: wards check_people_references_before_delete; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER check_people_references_before_delete BEFORE DELETE ON public.wards FOR EACH ROW EXECUTE FUNCTION public.prevent_ward_deletion_if_referenced();


--
-- TOC entry 4693 (class 2620 OID 33396)
-- Name: diagnosis check_references_before_delete; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER check_references_before_delete BEFORE DELETE ON public.diagnosis FOR EACH ROW EXECUTE FUNCTION public.prevent_deletion_if_referenced();


--
-- TOC entry 4694 (class 2620 OID 32984)
-- Name: diagnosis diagnosis_ref_trigger; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER diagnosis_ref_trigger BEFORE UPDATE ON public.diagnosis FOR EACH ROW EXECUTE FUNCTION public.check_diagnosis_ref();


--
-- TOC entry 4704 (class 2620 OID 33412)
-- Name: wards on_ward_diagnosis_change; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER on_ward_diagnosis_change BEFORE UPDATE OF diagnosis_id ON public.wards FOR EACH ROW EXECUTE FUNCTION public.handle_ward_diagnosis_change();


--
-- TOC entry 4696 (class 2620 OID 33394)
-- Name: people trigger_transfer_patient; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trigger_transfer_patient BEFORE UPDATE ON public.people FOR EACH ROW WHEN ((old.diagnosis_id IS DISTINCT FROM new.diagnosis_id)) EXECUTE FUNCTION public.transfer_patient_to_correct_ward();


--
-- TOC entry 4695 (class 2620 OID 33404)
-- Name: diagnosis validate_diagnosis_data; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER validate_diagnosis_data BEFORE INSERT OR UPDATE ON public.diagnosis FOR EACH ROW EXECUTE FUNCTION public.check_diagnosis_data();


--
-- TOC entry 4697 (class 2620 OID 33410)
-- Name: people validate_patient_ward; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER validate_patient_ward BEFORE INSERT OR UPDATE ON public.people FOR EACH ROW EXECUTE FUNCTION public.validate_patient_ward_diagnosis();


--
-- TOC entry 4698 (class 2620 OID 33400)
-- Name: people validate_people_data; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER validate_people_data BEFORE INSERT OR UPDATE ON public.people FOR EACH ROW EXECUTE FUNCTION public.check_people_data();


--
-- TOC entry 4699 (class 2620 OID 33406)
-- Name: people validate_people_references; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER validate_people_references BEFORE INSERT OR UPDATE ON public.people FOR EACH ROW EXECUTE FUNCTION public.check_people_references();


--
-- TOC entry 4705 (class 2620 OID 33408)
-- Name: wards validate_wards_references; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER validate_wards_references BEFORE INSERT OR UPDATE ON public.wards FOR EACH ROW EXECUTE FUNCTION public.check_wards_references();


--
-- TOC entry 4700 (class 2620 OID 32951)
-- Name: people ward_capacity_trigger; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER ward_capacity_trigger BEFORE INSERT ON public.people FOR EACH ROW EXECUTE FUNCTION public.check_ward_capacity();


--
-- TOC entry 4701 (class 2620 OID 33389)
-- Name: people ward_capacity_trigger_1; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER ward_capacity_trigger_1 BEFORE UPDATE ON public.people FOR EACH ROW EXECUTE FUNCTION public.check_ward_capacity();


--
-- TOC entry 4690 (class 2606 OID 32845)
-- Name: people fk_people_diagnosis; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.people
    ADD CONSTRAINT fk_people_diagnosis FOREIGN KEY (diagnosis_id) REFERENCES public.diagnosis(id) ON DELETE CASCADE NOT VALID;


--
-- TOC entry 4691 (class 2606 OID 32850)
-- Name: people fk_people_wards; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.people
    ADD CONSTRAINT fk_people_wards FOREIGN KEY (ward_id) REFERENCES public.wards(id) ON DELETE CASCADE NOT VALID;


--
-- TOC entry 4692 (class 2606 OID 32878)
-- Name: wards fk_wards_diagnosis; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.wards
    ADD CONSTRAINT fk_wards_diagnosis FOREIGN KEY (diagnosis_id) REFERENCES public.diagnosis(id) NOT VALID;


-- Completed on 2024-12-13 16:05:31

--
-- PostgreSQL database dump complete
--

