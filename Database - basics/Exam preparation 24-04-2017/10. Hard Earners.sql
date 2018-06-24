SELECT TOP 2 m.FirstName + ' ' + m.LastName AS Mechanic,
             COUNT(j.MechanicId) AS Jobs
  FROM Mechanics AS m
  JOIN Jobs AS j ON j.MechanicId = m.MechanicId
 GROUP BY j.MechanicId, m.FirstName + ' ' + m.LastName, j.FinishDate
HAVING j.FinishDate IS NULL
 ORDER BY COUNT(j.MechanicId) DESC, j.MechanicId ASC