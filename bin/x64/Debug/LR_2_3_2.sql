DO $$
DECLARE
	people_max_count NUMERIC;

BEGIN
WITH diagnosis_counts AS (
    SELECT diagnosis_id, COUNT(*) AS people_count
    FROM people
    GROUP BY diagnosis_id
),
maxCount AS (
    SELECT MAX(people_count) AS max_count
    FROM diagnosis_counts
)
SELECT COUNT(*) INTO people_max_count FROM people
WHERE diagnosis_id IN (
    SELECT diagnosis_id
    FROM diagnosis_counts
    WHERE people_count = (SELECT max_count FROM MaxCount)
);
WITH diagnosis_counts AS (
    SELECT diagnosis_id, COUNT(*) AS people_count
    FROM people
    GROUP BY diagnosis_id
),
maxCount AS (
    SELECT MAX(people_count) AS max_count
    FROM diagnosis_counts
)
DELETE FROM people WHERE diagnosis_id IN (SELECT diagnosis_id
FROM diagnosis_counts
WHERE people_count = (SELECT max_count FROM maxCount));

IF people_max_count < 3 THEN
RAISE NOTICE 'Произведён откат транзакции: больных 
с самым популярным диагнозом оказалось меньше 3-х человек';
    ROLLBACK;  

END IF;

COMMIT;

END $$;