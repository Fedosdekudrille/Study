--1
SET IMPLICIT_TRANSACTIONS ON;
SET nocount on
IF  exists (SELECT * FROM  SYS.OBJECTS
            WHERE OBJECT_ID= object_id(N'DBO.X') )	            
	DROP table X;           
DECLARE @c int, @flag char = 'c';
SET IMPLICIT_TRANSACTIONS  ON
CREATE table X(K int);
INSERT X values (1),(2),(3);
SET @c = (select count(*) from X);
PRINT 'количество строк в таблице X: ' + cast( @c as varchar(2));
--SET @flag = 'r';
IF @flag = 'c'  commit;
ELSE   rollback;
SET IMPLICIT_TRANSACTIONS  OFF
IF  exists (SELECT * FROM  SYS.OBJECTS
            WHERE OBJECT_ID= object_id(N'DBO.X') )	
	PRINT 'таблица X есть';  
ELSE PRINT 'таблицы X нет'
--2
USE UNIVER;
BEGIN TRY
	BEGIN TRANSACTION
	INSERT AUDITORIUM(AUDITORIUM) 
				VALUES ('100000');
	ROLLBACK TRANSACTION;
END TRY
BEGIN CATCH
	print cast(error_number() as varchar(10)) + ': ' + cast(error_message() as varchar(50));
END CATCH
if(@@TRANCOUNT > 0)
	ROLLBACK TRAN;
SELECT AUDITORIUM FROM AUDITORIUM WHERE AUDITORIUM.AUDITORIUM = '100000';
DELETE AUDITORIUM WHERE AUDITORIUM.AUDITORIUM = '100000';
--3
BEGIN TRY
	BEGIN TRANSACTION
	INSERT PROGRESS(NOTE) 
				VALUES (1);
	SAVE TRAN POINT1;
	INSERT PROGRESS(NOTE) 
				VALUES (2);
	SAVE TRAN POINT2;
	INSERT PROGRESS(NOTE)
				VALUES (3);
	SAVE TRAN POINT2;
	INSERT PROGRESS(NOTE) 
			VALUES (3);
	ROLLBACK TRANSACTION POINT2;
	COMMIT TRAN;
END TRY
BEGIN CATCH
	print cast(error_number() as varchar(10)) + ': ' + cast(error_message() as varchar(50));
END CATCH
if(@@TRANCOUNT > 0)
	ROLLBACK TRAN;
SELECT NOTE FROM PROGRESS WHERE PROGRESS.NOTE < 4;
DELETE PROGRESS WHERE PROGRESS.NOTE < 4;
--4
--A--
set transaction isolation level READ UNCOMMITTED
begin transaction
-------------t1---------------------------------
SELECT @@SPID [SPID], * FROM AUDITORIUM_TYPE
							WHERE AUDITORIUM_TYPENAME = 'Новая лекционная';
COMMIT;
-------------t2---------------------------------
--B--	---по умолчаниб READ COMMITTED
BEGIN TRANSACTION
SELECT @@SPID [SPID] 
INSERT AUDITORIUM_TYPE values ('ЛК-НН', 'Новая лекционная')
UPDATE PROGRESS set NOTE = '2'
			WHERE NOTE = '3'
-------------t1---------------------------------
-------------t2---------------------------------
ROLLBACK;
SELECT @@TRANCOUNT;
--5
drop table ##temp;
create table ##temp --5. read commited (минус неподтвержденное чтение)
		(
			field int 
		)

insert into ##temp values
(0),
(1),
(2)

BEGIN TRAN;

UPDATE ##temp
SET field = field * 10

SELECT field 
FROM ##temp;

WAITFOR DELAY '00:00:10';

ROLLBACK;
--6
--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN;

SELECT field 
FROM ##temp;

WAITFOR DELAY '00:00:10';

SELECT field 
FROM ##temp;

COMMIT;
--7
--SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE  
BEGIN TRAN;

SELECT * FROM ##temp  

WAITFOR DELAY '00:00:10'  

SELECT * FROM ##temp

COMMIT;
--8
USE UNIVER
BEGIN TRAN
	INSERT AUDITORIUM(AUDITORIUM)
		VALUES('100000');
	BEGIN TRAN
		UPDATE AUDITORIUM SET AUDITORIUM_NAME = 'NA' WHERE AUDITORIUM.AUDITORIUM = '100000';
	COMMIT;
	SELECT AUDITORIUM FROM AUDITORIUM WHERE AUDITORIUM.AUDITORIUM = '100000';
	ROLLBACK;
SELECT AUDITORIUM FROM AUDITORIUM WHERE AUDITORIUM.AUDITORIUM = '100000';