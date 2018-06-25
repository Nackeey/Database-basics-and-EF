SELECT t.Id,
	   h.Name AS HotelName,
	   r.Type AS RoomType,
	   IIF(t.CancelDate IS NULL, SUM(h.BaseRate + r.Price) ,0.00) AS Revenue	  
  FROM Trips AS t
  JOIN Rooms AS r ON r.Id = t.RoomId
  JOIN Hotels AS h ON h.Id = r.HotelId
  JOIN Cities AS c ON c.Id = h.CityId
  JOIN AccountsTrips AS at ON at.TripId = t.Id
  GROUP BY t.Id, r.Type, t.CancelDate, h.Name
  ORDER BY RoomType, t.Id
  
 