--01. Employees with Salary Above 35000
CREATE PROCEDURE usp_GetEmployeesSalaryAbove35000 
AS
	SELECT e.FirstName, e.LastName
	FROM Employees AS e
	WHERE e.Salary > 35000

--02. Employees with Salary Above Number
CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber (@number DECIMAL(18, 4))
AS
	SELECT e.FirstName, e.LastName
	FROM Employees AS e
	WHERE e.Salary >= @number

--03. own Names Starting With
CREATE PROCEDURE usp_GetTownsStartingWith (@prefix NVARCHAR(30))
AS 
  SELECT t.Name
  FROM Towns AS t 	
  WHERE LEFT(Name, LEN(@prefix)) = @prefix

--04. Employees from Town
CREATE PROCEDURE usp_GetEmployeesFromTown (@townName NVARCHAR(50))
AS
	SELECT e.FirstName, e.LastName
	FROM Employees AS e
	JOIN Addresses AS a ON a.AddressID = e.AddressID
	JOIN Towns as t ON t.TownID = a.TownID
	WHERE t.Name = @townName

--05. Salary Level Function
CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4)) 
RETURNS NVARCHAR(7)
AS
	BEGIN
		DECLARE @salaryLevel NVARCHAR(7)
		IF(@salary < 30000)
			SET	@salaryLevel = 'Low'
		ELSE IF(@salary BETWEEN 30000 AND 50000)
			SET @salaryLevel = 'Average'
		ELSE 
			SET @salaryLevel = 'High'
	RETURN @salaryLevel		
	END

--06. Employees by Salary Level
CREATE PROCEDURE usp_EmployeesBySalaryLevel (@salaryLevel NVARCHAR(10))
AS
	SELECT e.FirstName, e.LastName
	FROM Employees AS e
	WHERE dbo.ufn_GetSalaryLevel (e.Salary) = @salaryLevel
		 
--07. Define Function
CREATE FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(50), @word NVARCHAR(30))
RETURNS BIT
AS
BEGIN
	  DECLARE @isComprised BIT = 0;
	  DECLARE @currentIndex INT = 1;
	  DECLARE @currentChar CHAR;
	  
	  WHILE(@currentIndex <= LEN(@word))
	  BEGIN
		SET @currentChar = SUBSTRING(@word, @currentIndex, 1)
		IF(CHARINDEX(@currentChar, @setOfLetters) = 0)
		  RETURN @isComprised;
		SET @currentIndex += 1;
	  END
	  RETURN @isComprised + 1; 
END

--08. Delete Employees and Departments
CREATE PROCEDURE usp_DeleteEmployeesFromDepartment (@departmentId INT) 
AS
BEGIN
DELETE FROM EmployeesProjects
WHERE EmployeeID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

ALTER TABLE Departments
ALTER COLUMN ManagerID INT

UPDATE Employees
SET ManagerID = NULL
WHERE ManagerID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

UPDATE Departments
SET ManagerID = NULL 
WHERE ManagerID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

DELETE FROM Employees
WHERE DepartmentID = @departmentId

DELETE FROM Departments
WHERE DepartmentID = @departmentId
END

SELECT COUNT(*)
FROM Employees
WHERE DepartmentID = @departmentId