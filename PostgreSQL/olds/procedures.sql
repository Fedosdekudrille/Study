CREATE OR REPLACE PROCEDURE regularAccess.addNewUser (
 Name varchar(20),
 Email varchar(50),
 Password varchar(50),
 UserId inout int default 1,
 Admin boolean default false
)
LANGUAGE plpgsql
SECURITY DEFINER
AS $$
BEGIN
  BEGIN
    INSERT INTO public.users (Name, Email, Password, Admin) 
    VALUES (Name, Email, Password, Admin) RETURNING Id INTO UserId;
  	EXCEPTION WHEN OTHERS THEN
    ROLLBACK;
    RAISE;
  END;
END;
$$;


create or replace procedure regularAccess.deleteUser
(UserId int)
LANGUAGE plpgSQL
SECURITY DEFINER
AS $$
begin
   delete from users 
   where Id = UserId;
    EXCEPTION WHEN OTHERS THEN
    ROLLBACK;
    RAISE;
   end;
$$;

CREATE OR REPLACE PROCEDURE regularAccess.updateUser
(
  updateUserId int,
  updateName varchar(20) default null,
  updateEmail varchar(50) default null,
  updatePassword varchar(100) default null,
  updateAdmin boolean default null
)
SECURITY DEFINER
LANGUAGE plpgsql
AS $$
DECLARE cursorId int; cursorName varchar(20); cursorEmail varchar(50); cursorPassword varchar(50); cursorAdmin boolean;
DECLARE updateCursor CURSOR FOR SELECT Id, name, email, password, admin FROM users FOR UPDATE;
BEGIN
	OPEN updateCursor;
	LOOP
		FETCH updateCursor INTO cursorId, cursorName, cursorEmail, cursorPassword, cursorAdmin;
		IF cursorId = updateUserId THEN
			UPDATE users SET name = COALESCE(updateName, cursorName), email = COALESCE(updateEmail, cursorEmail),
			password = COALESCE(updatePassword, cursorPassword), admin = COALESCE(updateAdmin, cursorAdmin)
            WHERE CURRENT OF updateCursor;
			return;
		END IF;
		EXIT WHEN NOT FOUND;
	END LOOP;
END;
$$;

create or replace procedure regularAccess.deleteUserMessage
(
	s_id int default 0,
	r_id int default 0
)
LANGUAGE plpgSQL
AS $$
	begin
	if s_id != 0
	delete from messages
	where sender_id = s_id;
	elseif 
   commit;
   end;
$$;
CREATE OR REPLACE PROCEDURE regularAccess.addNewArticle (
 article_name varchar(100),
 article_theme varchar(100),
 article_content varchar(10000),
 article_picture varchar(100),
 creator_id int,
 is_admin boolean default false,
 article_id inout int default 1
)
LANGUAGE plpgsql
SECURITY DEFINER
AS $$
BEGIN
  BEGIN
	INSERT INTO ARTICLE (Theme, user_id, creation_date, content, picture, name, is_confirmed)
	values(article_theme, creator_id, CURRENT_TIMESTAMP, article_content, article_picture, article_name, is_admin) RETURNING Id INTO article_id;
  	EXCEPTION WHEN OTHERS THEN
    ROLLBACK;
    RAISE;
  END;
END;
$$;
regularAccess.addNewArticle('name', 1, 11, CURRENT_TIMESTAMP, 'generate_random_string(50, 500)', 'picture')