ALTER SESSION SET "_ORACLE_SCRIPT" = true;

create tablespace TS_KFA_PDB
     DATAFILE 'TS_KFA_PDB.dat'
     SIZE 5M   
     AUTOEXTEND ON NEXT 5M  
     MAXSIZE 20M ;
     
create temporary tablespace TS_KFA_PDB_TEMP
     tempfile 'TS_KFA_PDB_TEMP.dat'
     SIZE 5M   
     AUTOEXTEND ON NEXT 5M  
     MAXSIZE 20M ;
     
CREATE ROLE RL_KFAPDBCORE;

grant create session, 
create table,   
create view,    
create procedure, 
drop any table, 
drop any view,  
drop any procedure to RL_KFAPDBCORE;

commit;

grant create session to RL_KFAPDBCORE;

CREATE PROFILE PF_KFAPDBCORE LIMIT  
PASSWORD_LIFE_TIME 180   
SESSIONS_PER_USER 3   
FAILED_LOGIN_ATTEMPTS 7  
PASSWORD_LOCK_TIME 1    
PASSWORD_REUSE_TIME 10
PASSWORD_GRACE_TIME DEFAULT   
CONNECT_TIME 180  
IDLE_TIME 30;

create user U1_KFA_PDB IDENTIFIED BY 12345
DEFAULT TABLESPACE TS_KFA_PDB
QUOTA UNLIMITED ON TS_KFA_PDB
TEMPORARY TABLESPACE TS_KFA_PDB_TEMP
ACCOUNT UNLOCK;

grant create session to U1_KFA_PDB;
grant create table to U1_KFA_PDB;


