-- 1. Триггера на вставку
-- Создать триггер, который не позволяет добавить больного в палату, 
-- если там нет свободного места

/*CREATE OR REPLACE FUNCTION check_ward_capacity()
RETURNS TRIGGER AS $$
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
$$ LANGUAGE plpgsql;


CREATE TRIGGER ward_capacity_trigger
BEFORE INSERT ON people
FOR EACH ROW
EXECUTE FUNCTION check_ward_capacity();*/

-- Пытаемся добавить больного в VIP-палату, где максимальная вместимость - 1,
-- и этот один человек уже есть
/*INSERT INTO people(first_name, last_name, father_name, diagnosis_id, ward_id)
  VALUES ('Иванова', 'Алла', 'Андреевна', 1, 3);*/

-- 2. Триггера на модификацию
-- Создать триггер, который не позволяет изменить наименование диагноза, 
-- если на него есть ссылки

/*CREATE OR REPLACE FUNCTION check_diagnosis_ref()
RETURNS TRIGGER AS $$
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
$$ LANGUAGE plpgsql;

CREATE TRIGGER diagnosis_ref_trigger
BEFORE UPDATE ON diagnosis
FOR EACH ROW
EXECUTE FUNCTION check_diagnosis_ref();
*/


/*UPDATE diagnosis
SET name = 'Гастрит'
WHERE name = 'Атеросклероз' ;*/


-- 3. Триггера на удаление
-- Создать триггер, который при удалении палаты переводит всех больных
-- в свободные палаты с тем же диагнозом. И в случае недостатка места, удаление отменяет

/*CREATE OR REPLACE FUNCTION transfer_patients_before_delete()
RETURNS TRIGGER AS $$
DECLARE
    current_ward_id INT;
    current_diagnosis_id INT;
    current_max_count INT;
    current_occupied_count INT;
    available_wards RECORD;
    total_patients INT;
BEGIN
    -- Получаем id и диагноз удаляемой палаты
    current_ward_id := OLD.id;
    current_diagnosis_id := OLD.diagnosis_id;

    -- Получаем количество пациентов в удаляемой палате
    SELECT COUNT(*) INTO total_patients
    FROM people
    WHERE ward_id = current_ward_id;
	IF total_patients > 0 THEN
		-- Проверяем, есть ли свободные палаты с тем же диагнозом
		FOR available_wards IN
			SELECT w.id, w.max_count, (w.max_count - (SELECT COUNT(*) FROM people WHERE ward_id = w.id)) AS available_spots
			FROM wards w
			WHERE w.diagnosis_id = current_diagnosis_id
			AND w.id <> current_ward_id
			ORDER BY available_spots DESC  -- Сортируем по количеству свободных мест
		LOOP
			-- Если есть достаточно мест для всех пациентов, переводим их
			IF available_wards.available_spots >= total_patients THEN
				-- Переводим пациентов в найденную палату
				UPDATE people
				SET ward_id = available_wards.id
				WHERE ward_id = current_ward_id;
				RETURN OLD;  -- Удаление успешно завершено
			END IF;
		END LOOP;
	END IF;

    -- Если не хватает мест, отменяем удаление
    RAISE EXCEPTION 'Недостаточно мест для перевода пациентов. Удаление отменено.';
END;
$$ LANGUAGE plpgsql;
*/

/*CREATE OR REPLACE FUNCTION transfer_patients_before_delete()
RETURNS TRIGGER AS $$
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
$$ LANGUAGE plpgsql;*/

/*
CREATE TRIGGER before_ward_delete
BEFORE DELETE ON wards
FOR EACH ROW
EXECUTE FUNCTION transfer_patients_before_delete();
*/

--DELETE FROM wards WHERE id = 6;
--DELETE FROM wards WHERE id = 5;

/*CREATE TRIGGER ward_capacity_trigger_1
BEFORE UPDATE ON people
FOR EACH ROW
EXECUTE FUNCTION check_ward_capacity();*/

--DROP TRIGGER ward_capacity_trigger;

/*CREATE OR REPLACE FUNCTION check_patient_diagnosis()
RETURNS TRIGGER AS $$
BEGIN
    -- Проверка, есть ли палата с таким ward_id
    IF NOT EXISTS (
        SELECT 1
        FROM wards
        WHERE id = NEW.ward_id
          AND diagnosis_id = NEW.diagnosis_id
    ) THEN
        -- Если палата с указанным ward_id и diagnosis_id не найдена, выбрасываем исключение
        RAISE EXCEPTION 'Диагноз % пациента с кодом % не соответствует диагнозу палаты с номером %',
            NEW.diagnosis_id, NEW.id, NEW.ward_id;
    END IF;

    -- Если проверка пройдена, разрешаем операцию
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;*/

/*CREATE TRIGGER trigger_check_patient_diagnosis
BEFORE INSERT OR UPDATE ON people
FOR EACH ROW
EXECUTE FUNCTION check_patient_diagnosis();*/

/*CREATE OR REPLACE FUNCTION check_patient_diagnosis()
RETURNS TRIGGER AS $$
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
$$ LANGUAGE plpgsql;*/


/*CREATE TRIGGER trigger_check_patient_diagnosis_1
BEFORE INSERT OR UPDATE ON people
FOR EACH ROW
EXECUTE FUNCTION check_patient_diagnosis();*/
--DROP TRIGGER trigger_check_patient_diagnosis_1 ON people;


/*CREATE OR REPLACE FUNCTION transfer_patient_to_correct_ward()
RETURNS TRIGGER AS $$
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
$$ LANGUAGE plpgsql;*/

/*CREATE TRIGGER trigger_transfer_patient
BEFORE UPDATE ON people
FOR EACH ROW
WHEN (OLD.diagnosis_id IS DISTINCT FROM NEW.diagnosis_id)
EXECUTE FUNCTION transfer_patient_to_correct_ward();*/

/*CREATE OR REPLACE FUNCTION prevent_deletion_if_referenced()
RETURNS TRIGGER AS $$
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
$$ LANGUAGE plpgsql;*/

/*-- Создание триггера на таблице diagnosis
CREATE TRIGGER check_references_before_delete
BEFORE DELETE ON diagnosis
FOR EACH ROW
EXECUTE FUNCTION prevent_deletion_if_referenced();*/

--DELETE FROM diagnosis WHERE id = 8;

/*CREATE OR REPLACE FUNCTION prevent_ward_deletion_if_referenced()
RETURNS TRIGGER AS $$
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
$$ LANGUAGE plpgsql;*/

-- Создание триггера на таблице wards
/*CREATE TRIGGER check_people_references_before_delete
BEFORE DELETE ON wards
FOR EACH ROW
EXECUTE FUNCTION prevent_ward_deletion_if_referenced();*/
--DELETE FROM wards WHERE id = 10;

/*CREATE OR REPLACE FUNCTION check_people_data()
RETURNS TRIGGER AS $$
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
$$ LANGUAGE plpgsql;*/


/*CREATE TRIGGER validate_people_data
BEFORE INSERT OR UPDATE ON people
FOR EACH ROW
EXECUTE FUNCTION check_people_data();*/

/*CREATE OR REPLACE FUNCTION check_wards_data()
RETURNS TRIGGER AS $$
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
$$ LANGUAGE plpgsql;*/

/*CREATE TRIGGER validate_wards_data
BEFORE INSERT OR UPDATE ON wards
FOR EACH ROW
EXECUTE FUNCTION check_wards_data();*/

--DROP TRIGGER validate_wards_data ON wards;

/*CREATE OR REPLACE FUNCTION check_diagnosis_data()
RETURNS TRIGGER AS $$
BEGIN
    -- Проверка на пустое значение поля name
    IF NEW.name IS NULL OR NEW.name = '' THEN
        RAISE EXCEPTION 'Поля записи не могут быть нулевыми';
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;*/

/*CREATE TRIGGER validate_diagnosis_data
BEFORE INSERT OR UPDATE ON diagnosis
FOR EACH ROW
EXECUTE FUNCTION check_diagnosis_data();*/

/*CREATE OR REPLACE FUNCTION check_people_references()
RETURNS TRIGGER AS $$
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
$$ LANGUAGE plpgsql;*/

/*CREATE TRIGGER validate_people_references
BEFORE INSERT OR UPDATE ON people
FOR EACH ROW
EXECUTE FUNCTION check_people_references();*/

/*CREATE OR REPLACE FUNCTION check_wards_references()
RETURNS TRIGGER AS $$
BEGIN
    -- Проверка на существование диагноза
    IF NOT EXISTS (SELECT 1 FROM diagnosis WHERE id = NEW.diagnosis_id) THEN
        RAISE EXCEPTION 'Диагноз с кодом % не существует', NEW.diagnosis_id;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;*/

/*CREATE TRIGGER validate_wards_references
BEFORE INSERT OR UPDATE ON wards
FOR EACH ROW
EXECUTE FUNCTION check_wards_references();*/

/*CREATE OR REPLACE FUNCTION validate_patient_ward_diagnosis()
RETURNS TRIGGER AS $$
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
$$ LANGUAGE plpgsql;*/

/*CREATE TRIGGER validate_patient_ward
BEFORE INSERT OR UPDATE ON people
FOR EACH ROW
EXECUTE FUNCTION validate_patient_ward_diagnosis();*/


/*CREATE OR REPLACE FUNCTION handle_ward_diagnosis_change()
RETURNS TRIGGER AS $$
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
$$ LANGUAGE plpgsql;*/

/*CREATE TRIGGER on_ward_diagnosis_change
BEFORE UPDATE OF diagnosis_id ON wards
FOR EACH ROW
EXECUTE FUNCTION handle_ward_diagnosis_change();*/

/*CREATE OR REPLACE FUNCTION handle_ward_capacity_change()
RETURNS TRIGGER AS $$
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
$$ LANGUAGE plpgsql;

DROP TRIGGER on_ward_capacity_change ON wards;*/


/*CREATE TRIGGER on_ward_capacity_change
BEFORE UPDATE OF max_count ON wards
FOR EACH ROW
EXECUTE FUNCTION handle_ward_capacity_change();*/

