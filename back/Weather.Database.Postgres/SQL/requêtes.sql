SELECT EXTRACT(YEAR FROM Date) As Year,
                               MIN(MaxTemperature) AS MinTemperature,
                               MAX(MinTemperature) AS MaxTemperature
                        FROM Temperatures
                        WHERE LocationId = 134
                        GROUP BY EXTRACT(YEAR FROM Date)
                        ORDER BY Year ASC
						
						
						
SELECT * FROM country

SELECT * FROM location

DELETE FROM Temperatures WHERE locationid IN
(SElECT locationid FROM location WHERE countryid=51)

SELECT * FROM location WHERE countryid=2

SELECT l.Name, t.* 
FROM temperatures t
INNER JOIN location l ON t.locationid=l.locationid
WHERE t.locationid=23

UPDATE Temperatures
SET MinTemperature = MaxTemperature,
    MaxTemperature = MinTemperature;

SELECT DISTINCT t.locationid, l.countryid 
FROM Temperatures t
INNER JOIN Location l ON l.locationid=t.locationid 

SELECT COUNT(1)
FROM Temperatures


