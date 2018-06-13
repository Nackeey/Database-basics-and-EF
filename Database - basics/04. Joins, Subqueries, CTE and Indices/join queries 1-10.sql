--01. Employee Address
SELECT TOP 5 e.EmployeeID, e.JobTitle, e.AddressID, a.AddressText
  FROM Employees AS e
JOIN Addresses AS a ON a.AddressID = e.AddressID
ORDER BY a.AddressID

--02. Addresses with Towns
SELECT TOP 50 e.FirstName, e.LastName, t.Name, a.AddressText
  FROM ((Employees AS e
  JOIN Addresses AS a ON a.AddressID = e.AddressID)
  JOIN Towns AS t ON t.TownID = a.TownID)
 ORDER BY e.FirstName, e.LastName

--03. Sales Employees
SELECT e.EmployeeID, e.FirstName, e.LastName, d.Name AS [DepartmentName]
  FROM Employees AS e
  JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
 WHERE e.DepartmentID = 3
 ORDER BY e.EmployeeID

--04. Employee Departments
SELECT TOP 5 e.EmployeeID, e.FirstName, e.Salary, d.Name
  FROM Employees AS e
 INNER JOIN Departments AS d ON d.DepartmentID = e.DepartmentID 
 WHERE e.Salary > 15000
 ORDER BY e.DepartmentID

--05. Employees Without Projects
SELECT TOP 3 e.EmployeeID, e.FirstName
  FROM Employees AS e
 WHERE e.EmployeeID NOT IN 
     (
      SELECT ep.EmployeeID
	  FROM EmployeesProjects AS ep
      WHERE ep.EmployeeID = e.EmployeeID
     )

--06. Employees Hired After
SELECT e.FirstName, e.LastName, e.HireDate, d.Name AS DeptName
  FROM Employees AS e
 INNER JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
 WHERE d.Name IN ('Sales', 'Finance') AND e.HireDate > '1/1/1999'

--07. Employees With Project
SELECT TOP 5 e.EmployeeID, e.FirstName, p.Name AS ProjectName
  FROM Employees AS e
  INNER JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
  INNER JOIN Projects AS p ON p.ProjectID = ep.ProjectID
  WHERE p.StartDate > '08/13/2002'
  AND p.EndDate IS NULL
  ORDER BY e.EmployeeID ASC
	 
--08. Employee 24
SELECT e.EmployeeID, e.FirstName,
    IIF(p.StartDate > '12/31/2004', NULL, p.Name) AS ProjectName
  FROM Employees AS e
 INNER JOIN EmployeesProjects AS ep ON ep.EmployeeID = e.EmployeeID
 INNER JOIN Projects AS p ON p.ProjectID = ep.ProjectID   
 WHERE e.EmployeeID = 24

--09. Employee Manager
SELECT e.EmployeeID, e.FirstName, e.ManagerID, m.FirstName
  FROM Employees AS e
  JOIN Employees AS m ON e.ManagerID = m.EmployeeID
 WHERE e.ManagerID IN (3, 7)
 ORDER BY e.EmployeeID ASC
 
--10. Employees Summary
SELECT TOP 50
              e.EmployeeID,
              e.FirstName + ' ' + e.LastName AS EmployeeName,
              m.FirstName + ' ' + m.LastName AS ManagerName,
	          d.Name AS DepartmentName
  FROM Employees AS e
 INNER JOIN Employees AS m ON e.ManagerID = m.EmployeeID
 INNER JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
 ORDER BY e.EmployeeID ASC     