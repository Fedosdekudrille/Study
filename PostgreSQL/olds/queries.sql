call regularaccess.addNewUser('Fedosdekudrille', 'фываыва@gmail.com', '1234');
call regularaccess.deleteUser(regularaccess.getUserIdByEmail('фываыва@gmail.com'));
call regularaccess.updateUser(13, 'NewName', null, 'newP');
select * from regularaccess.userExists('fedosdekudrille@gmail.com', '1234');
select * from users
delete from users
INSERT INTO topics (Name) values ('spells');
Select * from topics;

CREATE OR REPLACE FUNCTION generate_random_string(min_length int, max_length int, isQuery bool)
  RETURNS TEXT AS
$$
DECLARE
  string_length INT;
  random_string TEXT = '';
  i INT;
  random_int INT;
  random_char CHAR;
BEGIN
  string_length := floor(random() * (max_length - min_length)) + min_length; -- Generate random length between 50 and 500
  
  FOR i IN 1..string_length LOOP
  	random_int := floor(random() * (90 - 65)) + 65;
    random_char := chr(random_int); -- Generate random character between ASCII 32 and 126
    IF i = 1 OR random() < 0.8 THEN
      random_string := random_string || random_char;
    ELSE
      random_string := random_string || ' ' || random_char;
    END IF;
  END LOOP;
  
  RETURN random_string;
END;
$$
LANGUAGE plpgsql;
SELECT generate_random_string(5, 20)
INSERT INTO ARTICLE (Theme_id, user_id, creation_date, content, picture, name)
values(1, 11, CURRENT_TIMESTAMP, generate_random_string(50, 500), 'picture', generate_random_string(5, 20));
Select * from article;

CREATE OR REPLACE FUNCTION string_to_query_format(str text)
returns text
AS
$$
BEGIN
	RETURN regexp_replace(trim(str), ' {1,}', ' & ', 'g');
END;
$$
LANGUAGE plpgsql;

CREATE OR REPLACE PROCEDURE fill_articles()
LANGUAGE plpgsql
AS $$
BEGIN
  FOR i IN 1..10000 LOOP
	INSERT INTO ARTICLE (theme, user_id, creation_date, content, picture, name)
	values('spells', 1, CURRENT_TIMESTAMP, generate_random_string(50, 500), 'picture', generate_random_string(5, 20));
  END LOOP;
END;
$$;
call fill_articles();


SELECT *, ts_rank_cd(textsearchable_index_col, to_tsquery('aa & q')) as rank
FROM article
WHERE textsearchable_index_col @@ to_tsquery('aa & q')
ORDER BY rank DESC


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

