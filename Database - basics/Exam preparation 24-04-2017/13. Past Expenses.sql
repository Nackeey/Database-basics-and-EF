SELECT j.JobId AS JobId,
       ISNULL(SUM(p.Price * op.Quantity), 0) AS Total
  FROM Jobs AS j
  LEFT JOIN Orders AS o ON o.JobId = j.JobId
  LEFT JOIN OrderParts AS op ON op.OrderId = o.OrderId
  LEFT JOIN Parts AS p ON p.PartId = op.PartId
  WHERE j.Status = 'Finished'
  GROUP BY j.JobId, j.Status
  ORDER BY Total DESC, j.JobId ASC 
 
