SELECT wards.id, wards.name, wards.diagnosis_id, wards.max_count, count(people.last_name) FROM wards 
left join people
on people.ward_id = wards.id
group by wards.id
