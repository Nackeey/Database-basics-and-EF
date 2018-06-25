SELECT * FROM Accounts

SELECT TOP 5 c.Id, c.Name AS City, c.CountryCode AS Country, COUNT(a.CityId) AS Accounts
  FROM Cities AS c
  JOIN Accounts AS a ON a.CityId = c.Id
  GROUP By c.Id, c.Name, c.CountryCode
  ORDER BY Accounts DESC