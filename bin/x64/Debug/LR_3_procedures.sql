-- 1. Создать хранимую процедуру, выводящую все палаты, 
--    содержащие больных, и число больных в палате.

/*CREATE OR REPLACE PROCEDURE get_wards_with_patients()
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


CALL get_wards_with_patients();*/

-- 2. Создать хранимую процедуру, имеющую два параметра «диагноз1» и «диагноз2». 
-- Процедура должна возвращать палаты, где занятых мест меньше, чем среднее количество людей
-- в палатах с этими диагнозами.

/*CREATE OR REPLACE PROCEDURE available_wards(
    diagnosis_1 integer,
    diagnosis_2 integer
)
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

CALL available_wards(9, 11);
*/

-- 3. Создать хранимую процедуру со входным параметром «палата» и двумя выходными параметрами, 
-- возвращающими палату с таким же диагнозом и самым низким отношением занятого места 
-- к свободному и самое низкое отношение занятого места к свободному среди палат других диагнозов

CREATE OR REPLACE PROCEDURE get_ward_ratios(
    IN ward_1 INT,
    OUT same_diagnosis_ward_id INT,
    OUT lowest_other_diagnosis_ratio FLOAT
)
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

DO
$$
DECLARE
	ward_1 INT;
    same_diagnosis_ward_id INT;
    lowest_other_diagnosis_ratio FLOAT;
BEGIN
    CALL get_ward_ratios(13, same_diagnosis_ward_id, lowest_other_diagnosis_ratio);
    RAISE NOTICE 'same_diagnosis_ward_id: %,  lowest_other_diagnosis_ratio %', same_diagnosis_ward_id, lowest_other_diagnosis_ratio;    
END;
$$
LANGUAGE plpgsql;
