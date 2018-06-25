SELECT * FROM AccountsTrips
SELECT * FROM Trips

SELECT DISTINCT a.Id AS AccountId,
       a.FirstName + ' ' + a.LastName AS FullName,
	   MAX(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate)) AS [LongestTrip],
	   MIN(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate)) AS ShortestTrip
  FROM Accounts AS a
  JOIN AccountsTrips AS at ON at.AccountId = a.Id
  JOIN Trips AS t ON t.Id = at.TripId
  WHERE a.MiddleName IS NULL AND t.CancelDate IS NULL
  GROUP BY a.Id, a.FirstName + ' ' + a.LastName
  ORDER BY [LongestTrip] DESC, AccountId

