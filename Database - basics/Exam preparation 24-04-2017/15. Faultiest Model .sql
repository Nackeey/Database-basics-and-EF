SELECT TOP 1 WITH TIES m.Name AS Model,
           COUNT(j.JobId) AS [Times Serviced],
	      (SELECT ISNULL(SUM(p.Price * op.Quantity), 0)
	   FROM Orders AS o
	   JOIN OrderParts AS op ON op.OrderId = o.OrderId
	   JOIN Parts AS p ON p.PartId = op.PartId
	   JOIN Jobs AS j ON j.JobId = o.JobId
	   WHERE j.ModelId = m.ModelId) AS [Parts Total]
  FROM Models AS m
  JOIN Jobs AS j ON j.ModelId = m.ModelId
  GROUP BY m.Name, m.ModelId
  ORDER BY [Times Serviced] DESC
  
