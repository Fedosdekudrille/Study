

---------1----------
SELECT * FROM dba_tablespaces; 
---------2------------
SELECT * FROM dba_data_files;

SELECT * FROM dba_TEMP_FILES;

SELECT * FROM dba_UNDO_EXTENTS;

---------3------------
SELECT * FROM V$LOG;

---------4------------
SELECT * FROM V$LOGFILE;
----------5------------
ALTER SYSTEM SWITCH LOGFILE;
SELECT * FROM V$LOG;
----------6------------
alter database add logfile group 4 'REDO0004.LOG' size 50 m blocksize 512;
alter database add logfile member 'REDO041.LOG' reuse  to group 4;
alter database add logfile member  'REDO042.LOG'  reuse to group 4;
alter database drop logfile group 4 ;
ALTER DATABASE ADD LOGFILE GROUP 4 ('/u01/app/oracle/oradata/mydb/redo04a.log', '/u01/app/oracle/oradata/mydb/redo04b.log', '/u01/app/oracle/oradata/mydb/redo04c.log') SIZE 100M;


SELECT group#, thread#, sequence#, first_change#, next_change#
FROM v$log;
----------7------------
SELECT * FROM v$log WHERE GROUP# = 4;

ALTER DATABASE DROP LOGFILE GROUP 4;
ALTER SYSTEM SWITCH LOGFILE;

ALTER DATABASE DROP LOGFILE MEMBER 'REDO04.LOG';
ALTER DATABASE DROP LOGFILE MEMBER 'REDO041.LOG';
ALTER DATABASE DROP LOGFILE MEMBER 'REDO042.LOG';
alter database drop logfile group 4 ;

----------8------------
SELECT log_mode FROM v$database;
----------9---------------
select max(sequence#) from v$log;
-----------10--------
Shutdown imediate;
ALTER SYSTEM ARCHIVE LOG START;

-----------11---------------
--ALTER SYSTEM SET LOG_ARCHIVE_DEST_1 ='LOCATION=/archive'

ALTER SYSTEM SWITCH LOGFILE;
SELECT NAME FROM V$ARCHIVED_LOG WHERE APPLIED='YES';
SELECT SEQUENCE#, FIRST_CHANGE#, NEXT_CHANGE# FROM V$ARCHIVED_LOG;
SELECT SEQUENCE#, FIRST_CHANGE#, NEXT_CHANGE# FROM V$LOG;
-------------12---------------------
shutdown immediate;
startup mount;
alter database archivelog;
select name, log_mode from v$database;
alter database open;

alter database close;
shutdown mount;
shutdown immediate;
alter database noarchivelog;

ALTER SYSTEM ARCHIVE LOG STOP;
SELECT LOG_MODE FROM V$DATABASE;
---------------13------------------
select * from v$controlfile;
select * from v$log_history;
---------------14------------------
show parameter control;
SELECT * FROM dba_users;