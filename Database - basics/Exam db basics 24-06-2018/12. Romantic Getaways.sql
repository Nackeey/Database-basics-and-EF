SELECT a.Id, a.Email,
   c.Name AS City, COUNT(t.Id) AS Trips
    FROM Cities AS c
	JOIN Hotels AS h ON h.CityId = c.Id
	JOIN Rooms AS r ON r.HotelId = h.Id
	JOIN Trips AS t ON t.RoomId = r.Id
	JOIN AccountsTrips AS at ON at.TripId = t.Id
	JOIN Accounts AS a ON a.Id = at.AccountId
	WHERE a.CityId = h.CityId
	GROUP BY a.Id, c.Name, a.Email
	ORDER BY Trips DESC, a.Id ASC