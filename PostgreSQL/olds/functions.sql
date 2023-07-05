create or replace function userExists(seekEmail varchar(50), seekPassword varchar(50))
returns boolean
language plpgsql
as
$$
DECLARE cursorEmail varchar(50); cursorPassword varchar(50);
DECLARE checkCursor CURSOR FOR SELECT email, password FROM users FOR READ ONLY;
begin
	OPEN checkCursor;
	LOOP
		FETCH checkCursor INTO cursorEmail, cursorPassword;
		IF cursorEmail = seekEmail AND cursorPassword = seekPassword THEN
			CLOSE checkCursor;
			return true;
		END IF;
		EXIT WHEN NOT FOUND;
	END LOOP;
	CLOSE checkCursor;
	return false;
end;
$$;

create or replace function getUserIdByEmail(search_email varchar(50))
returns int
language plpgsql
as
$$
begin
	return (SELECT id
	FROM users
	WHERE email = search_email);
end;
$$;