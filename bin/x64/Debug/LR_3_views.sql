 -- 1.	Создать представление, отображающее всех больных.
/*CREATE OR REPLACE VIEW v1 AS SELECT people.id, people.first_name, people.last_name, 
 	people.father_name FROM people;
SELECT * FROM v1*/

-- 2.	Создать представление, отображающее все палаты, число занятых мест, 
--      вместимость и диагноз, под который палата выделена.
CREATE OR REPLACE VIEW v2 AS SELECT wards.id wards_id, wards.name wards_name, 
                  wards.max_count,  count(people.last_name) AS real_count,
				  diagnosis.id diagnosis_id, diagnosis.name diagnosis_name				
FROM wards
JOIN diagnosis ON wards.diagnosis_id = diagnosis.id
LEFT JOIN people ON people.ward_id = wards.id
GROUP BY diagnosis.id, wards.id;
SELECT * FROM v2

