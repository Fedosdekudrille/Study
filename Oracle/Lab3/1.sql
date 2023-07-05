SELECT * FROM DICT
    where COMMENTS like '%user%';

select * from DBA_HIST_PDB_INSTANCE;

select * from DBA_PDBS;

select * from DBA_USERS;

select name, open_mode, con_id from v$pdbs; select pdb_name, pdb_id, status from SYS.dba_pdbs;

CREATE USER C##KFA IDENTIFIED BY 12345;

GRANT CREATE SESSION TO C##KFA;
GRANT CREATE TABLE TO C##KFA;

 
GRANT INSERT INTO TABLE TO C##KFA;

CREATE USER C##TDS IDENTIFIED BY 12345;

grant all privileges to C##TDS;
SELECT name, open_mode FROM v$pdbs;
select * from v$session
WHERE TYPE='USER' AND STATUS='ACTIVE';
