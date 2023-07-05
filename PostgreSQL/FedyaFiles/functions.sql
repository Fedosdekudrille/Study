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

CREATE OR REPLACE FUNCTION generate_random_string(min_length int, max_length int, bool isQuery)
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

CREATE OR REPLACE FUNCTION string_to_query_format(str text)
returns text
AS
$$
BEGIN
	RETURN regexp_replace(trim(str), ' {1,}', ' & ', 'g');
END;
$$
LANGUAGE plpgsql;