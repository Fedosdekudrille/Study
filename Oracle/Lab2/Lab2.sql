ALTER SESSION SET "_oracle_script" = TRUE;
create tablespace TS_LDY
  datafile 'C:\Study\Oracle\Lab2\TS_LDY.dbf'
  size 7M
  autoextend on next 5M
  maxsize 20M;
  

create temporary tablespace TS_LDY_TEMP
  tempfile 'C:\Study\Oracle\Lab2\TS_LDY_TEMP.dbf'
  size 5M
  autoextend on next 3M
  maxsize 30M;

select tablespace_name, status, contents logging from dba_tablespaces;
select file_name, tablespace_name, status from  dba_data_files
union
select file_name, tablespace_name, status from dba_temp_files;

create role RL_LDYCORE;
grant create session,
      create table, drop any table, 
      create view, drop any view,
      create procedure, drop any procedure
      to RL_LDYCORE;

select * from dba_roles where role like 'RL%';
select * from dba_sys_privs where grantee = 'RL_LDYCORE';

create profile PF_LDYCORE limit
  password_life_time 180
  sessions_per_user 3
  failed_login_attempts 7
  password_lock_time 1
  password_reuse_time 10
  password_grace_time default
  connect_time 180
  idle_time 30;

select * from dba_profiles where profile = 'PF_LDYCORE';
select * from dba_profiles where profile = 'DEFAULT';

create user U_LDYCORE identified by 12345
  default tablespace TS_LDY quota unlimited on TS_LDY
  temporary tablespace TS_LDY_TEMP
  profile PF_LDYCORE
  account unlock 
  password expire;
grant RL_LDYCORE to U_LDYCORE;



CREATE TABLE LDY_U2( x number(3), s varchar2(50));

INSERT ALL
    INTO LDY_U2 (x, s) VALUES (1, 'a')
    INTO LDY_U2 (x, s) VALUES (2, 'b')
    INTO LDY_U2 (x, s) VALUES (3, 'c')
SELECT * FROM dual;
COMMIT;
SELECT * FROM LDY_U2;


create tablespace LDY_QDATA
  datafile 'C:\Study\Oracle\Lab2\LDY_QDATA.dbf'
  size 10M
  autoextend on next 5M
  maxsize 20M
  offline;
  
alter tablespace LDY_QDATA online;

create user LDY identified by 12345
  default tablespace LDY_QDATA quota 2M on LDY_QDATA
  temporary tablespace TS_LDY_TEMP
  profile PF_LDYCORE
  account unlock 
  password expire;
  alter user U_LDYCORE quota 1M on LDY_QDATA;
grant RL_LDYCORE to LDY;

create tablespace LDY_T1
  datafile 'C:\Study\Oracle\Lab2\LDY_T1.dbf'
  size 10M
  autoextend on next 5M
  maxsize 20M;

create table LDY_T2
( 
  x number(3), 
  s varchar2(50)
) tablespace LDY_T1;
alter tablespace LDY_T1 offline;

INSERT ALL
    INTO LDY_T2 (x, s) VALUES (1, 'a')
    INTO LDY_T2 (x, s) VALUES (2, 'b')
    INTO LDY_T2 (x, s) VALUES (3, 'c')
SELECT * FROM dual;
COMMIT;
alter tablespace LDY_T1 online;

select * from dba_tablespaces;
select * from LDY_t2;
      
