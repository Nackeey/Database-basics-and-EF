USE Gringotts

--01. Records' Count
SELECT COUNT (Id) FROM WizzardDeposits

--02. Longest Magic Wand
SELECT MAX (MagicWandSize) AS [LongestMagicWand]
  FROM WizzardDeposits

--03. Longest Magic Wand per Deposit Groups
SELECT DepositGroup, MAX(MagicWandSize) AS [LongestMagicWand]
  FROM WizzardDeposits
 GROUP BY DepositGroup
 
--04. Smallest Deposit Group per Magic Wand Size
SELECT TOP 2 DepositGroup 
  FROM WizzardDeposits
 GROUP BY DepositGroup
 ORDER BY AVG(MagicWandSize)
 
--05. Deposit Sum
SELECT DepositGroup, SUM(DepositAmount) AS [TotalSum]
  FROM WizzardDeposits
 GROUP BY DepositGroup

--06. Deposits Sum for Ollivander Family
SELECT DepositGroup, SUM(DepositAmount) AS [TotalSum]
  FROM WizzardDeposits
 WHERE MagicWandCreator = 'Ollivander family'
 GROUP BY DepositGroup
 
--07. Deposit Filter
SELECT DepositGroup, SUM(DepositAmount) AS [TotalSum]
  FROM WizzardDeposits
 WHERE MagicWandCreator = 'Ollivander family'
 GROUP BY DepositGroup
HAVING SUM(DepositAmount) < 150000
 ORDER BY TotalSum DESC         

--08. Deposit Charge
SELECT DepositGroup, MagicWandCreator, MIN(DepositCharge) AS [MinDepositCharge]
  FROM WizzardDeposits
 GROUP BY DepositGroup, MagicWandCreator

--09. Age Groups
SELECT
  CASE
      WHEN Age < 10 THEN '[0-10]'
	  WHEN Age > 10 AND Age <= 20 THEN '[11-20]'
	  WHEN Age > 20 AND Age <= 30 THEN '[21-30]'
	  WHEN Age > 30 AND Age <= 40 THEN '[31-40]'
	  WHEN Age > 40 AND Age <= 50 THEN '[41-50]'
	  WHEN Age > 50 AND Age <= 60 THEN '[51-60]'
	  WHEN Age > 60 THEN '[61+]'
   END AS 'AgeGroup', COUNT('AgeGroup') AS [WizardCount]
  FROM WizzardDeposits
 GROUP BY
  CASE
      WHEN Age < 10 THEN '[0-10]'
	  WHEN Age > 10 AND Age <= 20 THEN '[11-20]'
	  WHEN Age > 20 AND Age <= 30 THEN '[21-30]'
	  WHEN Age > 30 AND Age <= 40 THEN '[31-40]'
	  WHEN Age > 40 AND Age <= 50 THEN '[41-50]'
	  WHEN Age > 50 AND Age <= 60 THEN '[51-60]'
	  WHEN Age > 60 THEN '[61+]'
   END

--10. First Letter
SELECT DISTINCT LEFT(FirstName, 1) AS [FirstLetter]
  FROM WizzardDeposits
  WHERE DepositGroup = 'Troll Chest'
  ORDER BY FirstLetter ASC

--11. Average Interest
SELECT DepositGroup, IsDepositExpired, AVG(DepositInterest) AS [AverageInterest]
  FROM WizzardDeposits
 WHERE DepositStartDate > '1985-01-01'
 GROUP BY DepositGroup, IsDepositExpired
 ORDER BY DepositGroup DESC, IsDepositExpired ASC

--12. Rich Wizard, Poor Wizard
SELECT SUM(DepoDifference) AS SumDifference
FROM 
  (SELECT
     FirstName AS [Host Wizard], 
     DepositAmount AS [Host Wizard Deposit],
     LEAD(FirstName) OVER (ORDER BY Id) AS [Guest Wizzard],
     LEAD(DepositAmount) OVER (ORDER BY Id) AS [Guest Wizard Deposit],
     DepositAmount - LEAD(DepositAmount) OVER (ORDER BY Id) AS [DepoDifference]
   FROM WizzardDeposits) AS Differences

--13. 
SELECT DepartmentID, SUM(Salary) AS [TotalSalary]
  FROM Employees
 GROUP BY DepartmentID

--14. Employees Minimum Salaries
SELECT DepartmentID, MIN(Salary) AS [MinumumSalary]
  FROM Employees
 WHERE DepartmentID IN (2, 5 ,7)
 GROUP BY DepartmentID

--15. Employees Average Salaries
SELECT * INTO NewTable
  FROM Employees
 WHERE Salary > 30000

DELETE FROM NewTable
 WHERE ManagerID = 42

UPDATE NewTable
SET Salary += 5000
WHERE DepartmentID = 1

SELECT DepartmentID, AVG(Salary) AS [AverageSalary]
  FROM NewTable
 GROUP BY DepartmentID

--16 Employees Maximum Salaries
SELECT DepartmentID, MAX(Salary) AS [MaxSalary]
 FROM Employees
 GROUP BY DepartmentID 
HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000

--17. Employees Count Salaries
SELECT COUNT(Salary) AS [Count]
  FROM Employees
 WHERE ManagerID IS NUll

--18. 3rd Highest Salary
SELECT DepartmentID, Salary AS [ThirdHighestSalary] FROM
(
	SELECT
		DepartmentID,
		MAX(Salary) AS Salary,
		DENSE_RANK() OVER(PARTITION BY DepartmentID ORDER BY Salary DESC) AS Rank
	FROM Employees
	GROUP BY DepartmentID, Salary
) AS ThirdPart
WHERE Rank = 3

--19. Salary Challenge
SELECT TOP 10 e1.FirstName, e1.LastName, e1.DepartmentID FROM Employees AS e1 
WHERE Salary > (SELECT AVG(Salary) FROM Employees AS e2 WHERE e2.DepartmentID = e1.DepartmentID
GROUP BY DepartmentID)

