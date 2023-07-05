
CREATE SCHEMA regularAccess;
create schema adminAccess;

CREATE OR REPLACE FUNCTION regularAccess.my_procedure1()
RETURNS void AS $$
BEGIN
  RAISE NOTICE 'This is my procedure 1';
END;
$$ LANGUAGE plpgsql;


ALTER PROCEDURE addnewuser SET SCHEMA regularAccess;
ALTER PROCEDURE deleteuser SET SCHEMA regularAccess;
ALTER PROCEDURE updateuser SET SCHEMA regularAccess;
ALTER FUNCTION userexists SET SCHEMA regularAccess;
ALTER FUNCTION getuseridbyemail SET SCHEMA regularAccess;

call regularAccess.addNewUser('Fedosdekudrille', 'fedosdekudrille@gmail.com', '1234');
CREATE ROLE regularUser;
create role adminUser;
GRANT USAGE ON SCHEMA regularAccess TO regularUser;
GRANT EXECUTE ON ALL FUNCTIONS IN SCHEMA regularAccess TO regularUser;
GRANT EXECUTE ON ALL PROCEDURES IN SCHEMA regularAccess TO regularUser;
GRANT USAGE ON SCHEMA regularAccess TO adminUser;
GRANT EXECUTE ON ALL FUNCTIONS IN SCHEMA regularAccess TO adminUser;
GRANT EXECUTE ON ALL PROCEDURES IN SCHEMA regularAccess TO adminUser;
GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO regularUser;
GRANT USAGE, SELECT ON SEQUENCE users_id_seq TO regularUser;
CREATE USER serverUser WITH PASSWORD 'qwerty123456';
GRANT regularUser TO serverUser;
create user adminUser with password 'qwerty123456';
grant regularUser to 



