  SELECT * FROM Likes WHERE CommentId NOT IN (SELECT Id FROM Comments)
