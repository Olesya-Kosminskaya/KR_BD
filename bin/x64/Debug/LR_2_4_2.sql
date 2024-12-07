DO $$
DECLARE
	people_count NUMERIC;
	ward_m_count NUMERIC;
BEGIN

UPDATE wards
SET max_count = '3'
WHERE id = '7' ;

SELECT COUNT(*) INTO people_count FROM people
WHERE people.ward_id = '7';

SELECT max_count INTO ward_m_count FROM wards
WHERE wards.id = '7';

IF ward_m_count < people_count THEN
RAISE NOTICE 'Произведён откат транзакции: больных 
больных оказалось больше, чем установленная вместимость';
    ROLLBACK;  

END IF;

COMMIT;

END $$;