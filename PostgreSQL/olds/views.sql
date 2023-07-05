CREATE or REPLACE VIEW fullArticles AS
SELECT t.name, a.content, a.picture, a.creation_date, l.likes_count
FROM article a
INNER JOIN topics t ON t.id = a.theme_id
LEFT JOIN articleLikes l ON l.id = a.id
ORDER BY a.creation_date DESC;
  
CREATE OR REPLACE VIEW articleLikes AS 
SELECT a.id, COUNT(l.id) AS likes_count 
FROM article a 
INNER JOIN topics t ON t.id = a.theme_id
LEFT JOIN article_likes l ON a.id = l.article_id 
GROUP BY a.id 
ORDER BY a.creation_date DESC;


CREATE or REPLACE VIEW fullMessages AS
SELECT m.id, t.name as thread, m.content, m.picture, u.id senderId, u.Name as sender, m.creation_date, s.message_likes
FROM messages m
INNER JOIN threads t ON t.id = m.thread_id
INNER JOIN users u ON u.id = m.user_id
LEFT JOIN (SELECT l.Message_id, COUNT(l.Message_id) as message_likes
		FROM messages_likes l
		GROUP BY l.Message_id) s
	ON s.Message_id = m.id
ORDER BY m.creation_date DESC;