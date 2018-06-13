--11. Min Average Salary
SELECT MIN(a.AverageSalary) AS MinAverageSalary
  FROM 
     (
      SELECT AVG(e.Salary) AS AverageSalary
	  FROM Employees AS e
      GROUP BY e.DepartmentID 
     ) AS a

--12. Highest Peaks in Bulgaria
SELECT mc.CountryCode, m.MountainRange,
       p.PeakName, p.Elevation
  FROM MountainsCountries AS mc  
 INNER JOIN Mountains AS m ON mc.MountainId = m.Id
 INNER JOIN Peaks AS p ON mc.MountainId = p.MountainId
 WHERE CountryCode = 'BG' 
   AND p.Elevation > 2835
 ORDER BY p.Elevation DESC

--13. Count Mountain Ranges
SELECT mc.CountryCode,
 COUNT(m.MountainRange) AS MountainRanges
  FROM MountainsCountries AS mc
 INNER JOIN Mountains AS m ON mc.MountainId = m.Id
 WHERE mc.CountryCode IN ('BG', 'US', 'RU')
 GROUP BY mc.CountryCode

--14. Countries With or Without Rivers
SELECT TOP 5
           c.CountryName,
		   r.RiverName
	   FROM Countries AS c
       LEFT JOIN CountriesRivers AS cr ON c.CountryCode = cr.CountryCode
	   LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
	   WHERE c.ContinentCode = 'AF'
	   ORDER BY c.CountryName ASC

--15. *Continents and Currencies
WITH CCYContUsage_CTE (ContinentCode, CurrencyCode, CurrencyUsage) AS (
  SELECT ContinentCode, CurrencyCode, COUNT(CountryCode) AS CurrencyUsage
  FROM Countries 
  GROUP BY ContinentCode, CurrencyCode
  HAVING COUNT(CountryCode) > 1  
)
SELECT ContMax.ContinentCode, ccy.CurrencyCode, ContMax.TopCCYUsage
FROM
  (SELECT ContinentCode, MAX(CurrencyUsage) AS TopCCYUsage
   FROM CCYContUsage_CTE 
   GROUP BY ContinentCode) AS ContMax
JOIN CCYContUsage_CTE AS ccy 
ON (ContMax.ContinentCode = ccy.ContinentCode AND ContMax.TopCCYUsage = ccy.CurrencyUsage)
ORDER BY ContMax.ContinentCode

--16. Countries Without any Mountains
SELECT COUNT(c.CountryCode) - COUNT(mc.CountryCode) AS CountryCode
  FROM Countries AS c
  LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode

--17. Highest Peak and Longest River by Country
SELECT TOP 5 
       c.CountryName,
       MAX(p.Elevation) AS HighestPeakElevation,
	   MAX(r.Length) AS LongestRiverLength
  FROM Countries AS c  	
  JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
  JOIN Peaks AS p ON p.MountainId = mc.MountainId
  JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode
  JOIN Rivers AS r ON r.Id = cr.RiverId
  GROUP BY c.CountryName
  ORDER BY HighestPeakElevation DESC, LongestRiverLength DESC, c.CountryName ASC 

--18. *Highest Peak Name and Elevation by Country
WITH PeaksMountains_CTE (Country, PeakName, Elevation, Mountain) AS (
  SELECT c.CountryName, p.PeakName, p.Elevation, m.MountainRange
  FROM Countries AS c
  LEFT JOIN MountainsCountries as mc ON c.CountryCode = mc.CountryCode
  LEFT JOIN Mountains AS m ON mc.MountainId = m.Id
  LEFT JOIN Peaks AS p ON p.MountainId = m.Id
)
SELECT TOP 5
  TopElevations.Country AS Country,
  ISNULL(pm.PeakName, '(no highest peak)') AS HighestPeakName,
  ISNULL(TopElevations.HighestElevation, 0) AS HighestPeakElevation,	
  ISNULL(pm.Mountain, '(no mountain)') AS Mountain
FROM 
  (SELECT Country, MAX(Elevation) AS HighestElevation
   FROM PeaksMountains_CTE 
   GROUP BY Country) AS TopElevations
LEFT JOIN PeaksMountains_CTE AS pm 
ON (TopElevations.Country = pm.Country AND TopElevations.HighestElevation = pm.Elevation)
ORDER BY Country, HighestPeakName





