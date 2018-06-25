SELECT * FROM Accounts

SELECT RTRIM(COALESCE(FirstName+ ' ','') + COALESCE(MiddleName+ ' ','') +COALESCE(LastName+ ' ','')) AS [Full Name],
       YEAR(BirthDate) AS [BirthYear]
  FROM Accounts
 WHERE YEAR(BirthDate) > 1991 
 ORDER BY YEAR(BirthDate) DESC, FirstName ASC