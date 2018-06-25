SELECT * FROM Hotels


SELECT c.Name AS City,
       COUNT(h.CityId) AS [Hotels]
  FROM Cities AS c
  FULL JOIN Hotels AS h ON h.CityId = c.Id
  GROUP BY c.Name
  ORDER BY Hotels DESC, c.Name ASC



