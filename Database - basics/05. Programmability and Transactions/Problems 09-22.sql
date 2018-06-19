--09. Find Full Name
CREATE PROCEDURE usp_GetHoldersFullName 
AS
  SELECT ah.FirstName + ' ' + ah.LastName AS [Full Name]
  FROM AccountHolders AS ah

--10. People with Balance Higher Than
CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan (@number DECIMAL(18, 4))
AS 
  BEGIN
    SELECT ah.FirstName, ah.LastName
	FROM AccountHolders AS ah
	JOIN Accounts AS a ON a.AccountHolderId = ah.Id
	GROUP BY ah.FirstName, ah.LastName
	HAVING SUM(a.Balance) > @number
  END

--11. Future Value Function
CREATE FUNCTION ufn_CalculateFutureValue (@sum DECIMAL(15, 2), @interestRate FLOAT, @years INT)
RETURNS DECIMAL(15, 4)
BEGIN
	RETURN @sum * POWER((1 + @interestRate), @years) 
END

--12. 
CREATE PROCEDURE usp_CalculateFutureValueForAccount (@accountID INT, @interestRate FLOAT) AS
BEGIN
	SELECT *
	FROM Accounts AS a
	JOIN AccountHolders AS ah ON ah.Id = a.AccountHolderId
	WHERE a.Id = @accountID	
END

--12. Calculating Interest
CREATE PROCEDURE usp_CalculateFutureValueForAccount (@accountId INT, @interestRate FLOAT) AS
BEGIN
	SELECT a.Id,
	       ah.FirstName, 
		   ah.LastName, 
		   a.Balance, 
		   dbo.ufn_CalculateFutureValue(a.Balance, @interestRate, 5)
	  FROM Accounts AS a
	  JOIN AccountHolders AS ah ON ah.Id = a.AccountHolderId 
	  WHERE a.Id = @accountId
END

--13. *Cash in User Games Odd Rows
CREATE FUNCTION ufn_CashInUsersGames (@gameName VARCHAR(50))
RETURNS TABLE
AS 
RETURN( 
SELECT SUM(e.Cash) AS SumCash
  FROM ( 
	SELECT g.Id, ug.Cash, ROW_NUMBER() OVER(ORDER BY ug.Cash DESC) AS [Row Number]
	FROM Games AS g
	JOIN UsersGames AS ug ON ug.GameId = g.Id
	WHERE g.Name = @gameName) AS e
	WHERE e.[Row Number] % 2 = 1
	)

--14. Create Table Logs
CREATE TABLE Logs(
  LogId INT PRIMARY KEY IDENTITY,
  AccountId INT FOREIGN KEY REFERENCES Accounts(Id),
  OldSum DECIMAL(15, 2) NOT NULL,
  NewSum DECIMAL(15, 2) NOT NULL
)

CREATE TRIGGER tr_AccountBalanceChange ON Accounts FOR UPDATE
AS 
BEGIN
  DECLARE @accountId INT = (SELECT Id FROM inserted);
  DECLARE @oldBalance DECIMAL(15, 2) = (SELECT Balance FROM deleted);
  DECLARE @newBalance DECIMAL(15, 2) = (SELECT Balance FROM inserted);
  IF(@newBalance <> @oldBalance)
	INSERT INTO Logs VALUES (@accountId, @oldBalance, @newBalance);
END

--15. Create Table Emails
CREATE TABLE NotificationEmails	(
  Id INT PRIMARY KEY IDENTITY,	
  Recipient INT FOREIGN KEY REFERENCES Accounts(Id),
  Subject VARCHAR(255) NOT NULL,
  Body VARCHAR(255) NOT NULL
)

CREATE TRIGGER tr_Notification ON Logs FOR INSERT
AS
BEGIN
  DECLARE @recipient INT  = (SELECT AccountId FROM inserted);
  DECLARE @oldBalance DECIMAL = (SELECT OldSum FROM inserted);
  DECLARE @newBalance DECIMAL = (SELECT NewSum FROM inserted);
  DECLARE @subject VARCHAR(255) = CONCAT('Balance change for account: ', @recipient);
  DECLARE @body VARCHAR(255) = CONCAT('On ', GETDATE(), ' your balance was changed from ',
                                       @oldBalance, ' to ', @newBalance, ' .');
  INSERT INTO NotificationEmails VALUES
    (@recipient, @subject, @body)		
END

--16. Deposit Money
CREATE OR ALTER PROCEDURE usp_DepositMoney (@accountId INT, @moneyAmount DECIMAL(18, 4))
AS
BEGIN
  BEGIN TRANSACTION
  UPDATE Accounts
  SET Balance += @moneyAmount
  WHERE Id = @accountId 

  IF(@@ROWCOUNT <> 1)
    BEGIN
	  ROLLBACK;
	  RAISERROR('Invalid account!', 16, 1);
	  RETURN;
	END
  COMMIT;
END

--17. Withdraw Money Procedure
CREATE PROCEDURE usp_WithdrawMoney (@accountId INT, @moneyAmount DECIMAL(18, 4))
AS
BEGIN
  BEGIN TRANSACTION
  UPDATE Accounts
  SET Balance -= @moneyAmount
  WHERE Id = @accountId

  IF(@@ROWCOUNT <> 1)
    BEGIN
	  ROLLBACK;
	  RAISERROR('Invalid account!', 16, 1);
	  RETURN;
	END
  COMMIT;
END

--18. Money Transfer
CREATE PROCEDURE usp_TransferMoney (@senderId INT, @receiverId INT, @amount DECIMAL(18, 4))
AS
BEGIN 
  BEGIN TRANSACTION
	EXEC usp_WithdrawMoney @senderId, @amount;
    EXEC usp_DepositMoney @receiverId, @amount;
	DECLARE @senderBalance DECIMAL(18, 4) = (SELECT Balance FROM Accounts WHERE Id = @senderId)
	IF(@senderBalance <= 0)
	BEGIN
	  ROLLBACK;
	  RAISERROR('Insufficient money', 16, 1);
	  RETURN;
	END  
  COMMIT;
END   

--21. Employees with Three Projects
CREATE PROCEDURE usp_AssignProject(@employeeId INT, @projectID INT)
AS
  BEGIN
    BEGIN TRAN
    INSERT INTO EmployeesProjects
    VALUES (@employeeId, @projectID)
    IF (SELECT COUNT(ProjectID)
        FROM EmployeesProjects
        WHERE EmployeeID = @employeeId) > 3
      BEGIN
        RAISERROR ('The employee has too many projects!', 16, 1)
        ROLLBACK
        RETURN
      END
    COMMIT
  END

--22. DELETE Employees
CREATE TABLE Deleted_Employees
(
  EmployeeId INT PRIMARY KEY IDENTITY,
  FirstName VARCHAR(50) NOT NULL,
  LastName VARCHAR(50) NOT NULL,
  MiddleName VARCHAR(50),
  JobTitle VARCHAR(50),
  DepartmentId INT,
  Salary DECIMAL(15, 2)
)

CREATE TRIGGER tr_DeleteEmployees
  ON Employees
  AFTER DELETE
AS
  BEGIN
    INSERT INTO Deleted_Employees
      SELECT
        FirstName,
        LastName,
        MiddleName,
        JobTitle,
        DepartmentID,
        Salary
      FROM deleted
  END
