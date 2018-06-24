SELECT m.ModelId,
       m.Name,
	   CAST(AVG(DATEDIFF(DAY, j.IssueDate, j.FinishDate)) as VARCHAR) + ' days' AS [Average Service Time]
  FROM Models AS m
  LEFT JOIN Jobs AS j ON j.ModelId = m.ModelId
  GROUP BY m.Name, m.ModelId
  ORDER BY AVG(DATEDIFF(DAY, j.IssueDate, j.FinishDate))