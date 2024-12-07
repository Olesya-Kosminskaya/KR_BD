-- Хранимая процедура для анализа заполненности:
-- Хранимая процедура имеет два параметра, определяющие анализируемый диапазон
-- отношения занятого места к свободному (верхняя и нижняя граница диапазона).
-- Результатом работы процедуры должна явиться выборка, содержащая среднюю заполненность
-- по всем палатам в рассматриваемом интервале заполненности в разрезе диагнозов.
-- Алгоритм реализации предлагается следующий. Организуется курсор, перебирающий все диагнозы,
-- больные с которыми находятся на лечении и относятся к палатам, попадающим в заданный
-- интервал заполненности. Для каждого диагноза считается средняя заполненность
-- и вместе с наименованием диагноза выводится в результирующую выборку.


CREATE OR REPLACE PROCEDURE analyze_ward_occupancy(
    IN lower_bound FLOAT,
    IN upper_bound FLOAT
)
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


CALL analyze_ward_occupancy(0.2, 0.5);
