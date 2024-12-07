 
(
    SELECT people.diagnosis_id
    FROM people
    GROUP BY people.diagnosis_id
    HAVING people.diagnosis_id = (SELECT MAX(people.diagnosis_id) FROM people)
)

 
SELECT diagnosis.id, diagnosis.name, count(people.last_name) FROM diagnosis 
left join people
on people.diagnosis_id = diagnosis.id
group by diagnosis.id

DO $$

WITH diagnosis_counts AS (
    SELECT diagnosis_id, COUNT(*) AS people_count
    FROM people
    GROUP BY diagnosis_id
),
MaxCount AS (
    SELECT MAX(people_count) AS max_count
    FROM diagnosis_counts
)

SELECT COUNT(*) INTO p_count WHERE (SELECT * FROM people WHERE diagnosis_id IN (SELECT diagnosis_id
FROM diagnosis_counts
WHERE people_count = (SELECT max_count FROM MaxCount)));
IF people_count > 3
THEN
RAISE NOTICE 'Произведён откат транзакции: больных 
с самым популярным диагнозом оказалось меньше 3-х человек';
END IF;
END $$;

--SELECT FROM people
--WHERE people.diagnosis_id IN (
  --  SELECT people.diagnosis_id
  --  FROM people
  --  GROUP BY people.diagnosis_id
 --   HAVING COUNT(*) = 1
--);
