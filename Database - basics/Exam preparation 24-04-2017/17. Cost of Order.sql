CREATE FUNCTION udf_GetCost (@jobId INT)
RETURNS DECIMAL(15, 2)
BEGIN
  DECLARE @totalCost DECIMAL(15, 2) = (
        SELECT SUM(p.Price * op.Quantity)
        FROM Jobs AS j
		JOIN Orders AS o ON o.JobId = j.JobId
		JOIN OrderParts AS op ON op.OrderId = o.OrderId
		JOIN Parts AS p ON p.PartId = op.PartId
       WHERE j.JobId = @jobId)

  IF (@totalCost IS NULL)
  BEGIN
    RETURN 0
  END

  RETURN @totalCost
END

