call regularaccess.addNewUser('Fedosdekudrille', 'фываыва@gmail.com', '1234');
call regularaccess.deleteUser(regularaccess.getUserIdByEmail('фываыва@gmail.com'));
call regularaccess.updateUser(13, 'NewName', null, 'newP');
select * from regularaccess.userExists('fedosdekudrille@gmail.com', '1234');
select * from users
delete from users
INSERT INTO topics (Name) values ('D&D FAQ');
Select * from topics;

INSERT INTO ARTICLE (Theme_id, user_id, creation_date, content, picture, likes)
values(2, 3, CURRENT_TIMESTAMP, 'content', 'picture', 1);
Select * from article;

INSERT INTO ARTICLE_likes(article_id, user_id) values (2,3);

Select Count(Article_id) from article_likes group by Article_id;
Select * from articleLikes order by id desc;

INSERT INTO threads (Name, creator_id) 
values('Looking for DM', 3);
Select * from threads;

INSERT INTO messages (thread_id, user_id, content, picture) values
(2, 3, 'i dont understand', 'pic');
Select * from messages;

INSERT INTO messages_likes (message_id, User_id) values
(2, 3)

SELECT * FROM fullMessages;