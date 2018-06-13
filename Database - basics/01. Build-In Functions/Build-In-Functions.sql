--01. Find Names of All Employees by First Name
SELECT FirstName, LastName
  FROM Employees
 WHERE FirstName LIKE 'SA%'

--02. Find Names of All employees by Last Name 
 SELECT FirstName, LastName
   FROM Employees
  WHERE LastName LIKE '%ei%'

--03. Find First Names of All Employess
SELECT FirstName
  FROM Employees 
 WHERE DepartmentID = 3 OR DepartmentID = 10
   AND HireDate BETWEEN '01-01-1995' AND '12-31-2005'

--04. Find All Employees Except Engineers
SELECT FirstName, LastName
  FROM Employees
 WHERE JobTitle NOT LIKE '%engineer%'

--05. Find Towns with Name Length
SELECT [Name]
  FROM Towns 
 WHERE LEN(Name) = 5 OR LEN(Name) = 6
 ORDER BY [Name] ASC

--06. Find Towns Starting With
SELECT TownId ,[Name]
  FROM Towns
 WHERE LEFT(Name, 1) IN ('M', 'K', 'B', 'E')
 ORDER BY [Name] ASC

 --07. Find Towns Not Starting With
SELECT TownId, [Name]
  FROM Towns
 WHERE LEFT(Name, 1) NOT IN ('R', 'B', 'D')
 ORDER BY [Name] ASC

--08. Create View Employees Hired After
CREATE VIEW [v_EmployeesHiredAfter2000] AS
SELECT FirstName, LastName
  FROM Employees
 WHERE HireDate > '12-31-2000'

--09. Length of Last Name
SELECT FirstName, LastName
  FROM Employees
 WHERE LEN(LastName) = 5

USE Geography

--10. Countries Holding 'A'
SELECT CountryName, IsoCode
  FROM Countries
 WHERE LEN(CountryName) - LEN(REPLACE(CountryName, 'a', '')) >= 3
 ORDER BY IsoCode

--11. Mix of Peak and River Names
SELECT PeakName, RiverName, LOWER(SUBSTRING(PeakName, 1, LEN(PeakName) - 1) + RiverName) AS Mix
  FROM Peaks, Rivers
 WHERE RIGHT(PeakName, 1) = LEFT(RiverName, 1)
 ORDER BY Mix ASC

USE Diablo

--12. Games from 2011 and 2012 year  
SELECT TOP 50 [Name], 
FORMAT([Start], 'yyyy-MM-dd') AS [Start]
  FROM Games
 WHERE YEAR([Start]) IN (2011, 2012)
 ORDER BY [Start], [Name]

--13. User Email Providers
SELECT Username,
 RIGHT(Email, LEN (Email) - CHARINDEX ('@', Email)) AS [Email Provider]
  FROM Users
 ORDER BY [Email Provider], Username
 
--14. Get Users with IPAddress Like Pattern
SELECT Username,
       IpAddress
  FROM Users
 WHERE IpAddress LIKE '___.1__.%.___'
 ORDER BY Username ASC

--15. Show All Games with Duration
SELECT [Name],
  CASE
      WHEN DATEPART (HOUR, [Start]) BETWEEN 0 AND 11 THEN 'Morning'
	  WHEN DATEPART (HOUR, [Start]) BETWEEN 12 AND 17 THEN 'Afternoon'
	  WHEN DATEPART (HOUR, [Start]) BETWEEN 18 AND 23 THEN 'Evening'
   END AS [Part of the Day],
  CASE
      WHEN Duration <= 3 THEN 'Extra Short' 
	  WHEN Duration BETWEEN 4 AND 6 THEN 'Short'
	  WHEN Duration IS NULL THEN 'Extra Long'
	  ELSE 'Long'
   END AS [Duration]
  FROM Games
 ORDER BY [Name], Duration, [Part of the Day] 

--16. Orders Table
SELECT ProductName, OrderDate,
      DATEADD(DAY, 3, OrderDate) AS [Pay Due],
	  DATEADD(MONTH, 1, OrderDate) AS [Deliver Due]
  FROM Orders	
