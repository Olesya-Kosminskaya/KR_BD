DO $$
DECLARE
	people_count NUMERIC;
	ward_count NUMERIC;
	places NUMERIC;
	ward_count3 NUMERIC;
BEGIN
SELECT COUNT(*) INTO people_count FROM people;
SELECT SUM(wards.max_count) INTO places FROM wards;
SELECT wards.max_count INTO ward_count3 FROM wards WHERE id = 3;

IF people_count = places THEN
RAISE NOTICE 'Произведён откат транзакции: все места в палатах заняты';
    ROLLBACK;
    
ELSE  

INSERT INTO people(first_name, last_name, father_name, diagnosis_id, ward_id)
  VALUES ('Сидоров', 'Анатолий', 'Семёнович', '11', '3');

UPDATE people
SET ward_id = '6'
WHERE ward_id = '3' 
AND EXISTS (
    SELECT * FROM people 
    WHERE (select count(*) from people where ward_id = 3) > (ward_count3)
);
INSERT INTO people(first_name, last_name, father_name, diagnosis_id, ward_id)
  VALUES ('Марков', 'Марк', 'Витальевич', '11', '3');

UPDATE people
SET ward_id = '6'
WHERE ward_id = '3' 
AND EXISTS (
    SELECT * FROM people 
    WHERE (select count(*) from people where ward_id = 3) > (ward_count3)
);
INSERT INTO people(first_name, last_name, father_name, diagnosis_id, ward_id)
  VALUES ('Карпов', 'Иван', 'Витальевич', '11', '3');

UPDATE people
SET ward_id = '6'
WHERE ward_id = '3' 
AND EXISTS (
    SELECT * FROM people 
    WHERE (select count(*) from people where ward_id = 3) > (ward_count3)
);

END IF;

COMMIT;

END $$;