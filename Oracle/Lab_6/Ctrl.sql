CREATE CONTROLFILE SET DATABASE "mydb" RESETLOGS NOARCHIVELOG
MAXLOGFILES 16
MAXLOGMEMBERS 3
MAXDATAFILES 100
MAXINSTANCES 8
MAXLOGHISTORY 292
LOGFILE
  GROUP 1 '/u01/oradata/mydb/redo01.log' SIZE 100M,
  GROUP 2 '/u01/oradata/mydb/redo02.log' SIZE 100M,
  GROUP 3 '/u01/oradata/mydb/redo03.log' SIZE 100M
DATAFILE
  '/u01/oradata/mydb/system01.dbf',
  '/u01/oradata/mydb/users01.dbf',
  '/u01/oradata/mydb/example01.dbf'
CHARACTER SET UTF8;