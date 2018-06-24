SELECT j.Status, j.IssueDate
  FROM Jobs AS j
 WHERE Status <> 'Finished'
 ORDER BY IssueDate, JobId