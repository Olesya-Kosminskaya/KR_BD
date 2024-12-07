/*INSERT INTO diagnosis(name)
  VALUES ('Склероз');*/
  
--DELETE FROM wards WHERE id = 5;


--DROP TRIGGER diagnosis_ref_trigger  
--ON diagnosis; 

CALL analyze_ward_occupancy(0.2, 0.5);

