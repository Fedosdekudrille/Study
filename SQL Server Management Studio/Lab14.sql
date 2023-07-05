USE UNIVER;

CREATE TABLE TR_ADULT
(
	ID int identity,
	STMT varchar(20) check (STMT in ('INS', 'DEL', 'UPD')),
	TRNAME varchar(50),
	CC varchar(300)
);
go
CREATE TRIGGER TR_TEACHER_INS ON TEACHER AFTER INSERT
as DECLARE @t char(10), @tn varchar(100), @g char(1), @p char(20), @str varchar(150);
print 'INSERT';
SELECT @t = (SELECT TEACHER FROM inserted), @tn = (SELECT TEACHER_NAME FROM inserted),
@g = (SELECT GENDER FROM inserted), @p = (SELECT PULPIT FROM inserted);
SET @str = @t + ' ' + @tn + ' ' + @g + ' ' + @p;
INSERT INTO TR_ADULT(STMT, TRNAME, CC)
					VALUES('INS', 'TR_TEACHER_INS', @str);
return;
go
CREATE TRIGGER TR_TEACHER_DEL ON TEACHER AFTER DELETE
as DECLARE @t char(10), @tn varchar(100), @g char(1), @p char(20), @str varchar(150);
print 'DELETE';
SELECT @t = (SELECT TEACHER FROM deleted), @tn = (SELECT TEACHER_NAME FROM deleted),
@g = (SELECT GENDER FROM deleted), @p = (SELECT PULPIT FROM deleted);
SET @str = @t + ' ' + @tn + ' ' + @g + ' ' + @p;
INSERT INTO TR_ADULT(STMT, TRNAME, CC)
					VALUES('DEL', 'TR_TEACHER_DEL', @str);
return;
go
CREATE TRIGGER TR_TEACHER_UPD ON TEACHER AFTER UPDATE
as DECLARE @t char(10), @tn varchar(100), @g char(1), @p char(20), @str varchar(150);
print 'UPDATE';
SELECT @t = (SELECT TEACHER FROM deleted), @tn = (SELECT TEACHER_NAME FROM deleted),
@g = (SELECT GENDER FROM deleted), @p = (SELECT PULPIT FROM deleted);
SET @str = @t + ' ' + @tn + ' ' + @g + ' ' + @p;
SELECT @t = (SELECT TEACHER FROM inserted), @tn = (SELECT TEACHER_NAME FROM inserted),
@g = (SELECT GENDER FROM inserted), @p = (SELECT PULPIT FROM inserted);
SET @str = @t + ' ' + @tn + ' ' + @g + ' ' + @p + ' ' + @str;
INSERT INTO TR_ADULT(STMT, TRNAME, CC)
					VALUES('UPD', 'TR_TEACHER_UPD', @str);
return;
go
INSERT INTO TEACHER
		VALUES('ASDF', 'ASDF FASD', 'м', 'ИСиТ');
UPDATE	TEACHER SET TEACHER_NAME = 'LKJHOI' WHERE TEACHER.TEACHER = 'ASDF';
DELETE TEACHER WHERE TEACHER.TEACHER = 'ASDF';
SELECT * FROM TR_ADULT;
DROP TRIGGER TR_TEACHER_INS;
DROP TRIGGER TR_TEACHER_DEL;
DROP TRIGGER TR_TEACHER_UPD;
go --4
create trigger TR_TEACHER on TEACHER after INSERT, DELETE, UPDATE  
as DECLARE @t char(10), @tn varchar(100), @g char(1), @p char(20), @str varchar(150);
declare @ins int = (select count(*) from inserted),
              @del int = (select count(*) from deleted); 
if  @ins > 0 and  @del = 0  
begin 
    print 'INSERT';
	SELECT @t = (SELECT TEACHER FROM inserted), @tn = (SELECT TEACHER_NAME FROM inserted),
	@g = (SELECT GENDER FROM inserted), @p = (SELECT PULPIT FROM inserted);
	SET @str = @t + ' ' + @tn + ' ' + @g + ' ' + @p;
	INSERT INTO TR_ADULT(STMT, TRNAME, CC)
						VALUES('INS', 'TR_TEACHER', @str);
end; 
else		  	 
if @ins = 0 and  @del > 0  
begin 
    print 'DELETE';
	SELECT @t = (SELECT TEACHER FROM deleted), @tn = (SELECT TEACHER_NAME FROM deleted),
	@g = (SELECT GENDER FROM deleted), @p = (SELECT PULPIT FROM deleted);
	SET @str = @t + ' ' + @tn + ' ' + @g + ' ' + @p;
	INSERT INTO TR_ADULT(STMT, TRNAME, CC)
					VALUES('DEL', 'TR_TEACHER', @str);
end; 
else	  
if @ins > 0 and  @del > 0  
begin 
    print 'UPDATE';
	SELECT @t = (SELECT TEACHER FROM deleted), @tn = (SELECT TEACHER_NAME FROM deleted),
	@g = (SELECT GENDER FROM deleted), @p = (SELECT PULPIT FROM deleted);
	SET @str = @t + ' ' + @tn + ' ' + @g + ' ' + @p;
	SELECT @t = (SELECT TEACHER FROM inserted), @tn = (SELECT TEACHER_NAME FROM inserted),
	@g = (SELECT GENDER FROM inserted), @p = (SELECT PULPIT FROM inserted);
	SET @str = @t + ' ' + @tn + ' ' + @g + ' ' + @p + ' ' + @str;
	INSERT INTO TR_ADULT(STMT, TRNAME, CC)
					VALUES('UPD', 'TR_TEACHER', @str);
end;  
return; 
go
INSERT INTO TEACHER
		VALUES('ASDF', 'ASDF FASD', 'м', 'ИСиТ');
UPDATE	TEACHER SET TEACHER_NAME = 'LKJHOI' WHERE TEACHER.TEACHER = 'ASDF';
DELETE TEACHER WHERE TEACHER.TEACHER = 'ASDF';
SELECT * FROM TR_ADULT;
DROP TRIGGER TR_TEACHER;
go --5
ALTER TABLE TEACHER ADD CONSTRAINT TEACHER_NAME CHECK(TEACHER.TEACHER_NAME != 'ASDF FASD')
go
INSERT INTO TEACHER 
		VALUES('ASDF', 'ASDF FASD', 'м', 'ИСиТ');
go
ALTER TABLE TEACHER DROP CONSTRAINT TEACHER_NAME;
SELECT * FROM TR_ADULT;
go --6
CREATE TRIGGER TR_TEACHER_DEL1 ON TEACHER AFTER DELETE
as DECLARE @t char(10), @tn varchar(100), @g char(1), @p char(20), @str varchar(150);
print 'DELETE';
SELECT @t = (SELECT TEACHER FROM deleted), @tn = (SELECT TEACHER_NAME FROM deleted),
@g = (SELECT GENDER FROM deleted), @p = (SELECT PULPIT FROM deleted);
SET @str = @t + ' ' + @tn + ' ' + @g + ' ' + @p;
INSERT INTO TR_ADULT(STMT, TRNAME, CC)
					VALUES('DEL', '1TR_TEACHER_DEL1', @str);
return;
go
CREATE TRIGGER TR_TEACHER_DEL2 ON TEACHER AFTER DELETE
as DECLARE @t char(10), @tn varchar(100), @g char(1), @p char(20), @str varchar(150);
print 'DELETE';
SELECT @t = (SELECT TEACHER FROM deleted), @tn = (SELECT TEACHER_NAME FROM deleted),
@g = (SELECT GENDER FROM deleted), @p = (SELECT PULPIT FROM deleted);
SET @str = @t + ' ' + @tn + ' ' + @g + ' ' + @p;
INSERT INTO TR_ADULT(STMT, TRNAME, CC)
					VALUES('DEL', '2TR_TEACHER_DEL2', @str);
return;
go
CREATE TRIGGER TR_TEACHER_DEL3 ON TEACHER AFTER DELETE
as DECLARE @t char(10), @tn varchar(100), @g char(1), @p char(20), @str varchar(150);
print 'DELETE';
SELECT @t = (SELECT TEACHER FROM deleted), @tn = (SELECT TEACHER_NAME FROM deleted),
@g = (SELECT GENDER FROM deleted), @p = (SELECT PULPIT FROM deleted);
SET @str = @t + ' ' + @tn + ' ' + @g + ' ' + @p;
INSERT INTO TR_ADULT(STMT, TRNAME, CC)
					VALUES('DEL', '3TR_TEACHER_DEL3', @str);
return;
go
exec  SP_SETTRIGGERORDER @triggername = 'TR_TEACHER_DEL3', 
	                        @order = 'First', @stmttype = 'DELETE';

exec  SP_SETTRIGGERORDER @triggername = 'TR_TEACHER_DEL2', 
	                        @order = 'Last', @stmttype = 'DELETE';
select t.name, e.type_desc 
       from sys.triggers  t join  sys.trigger_events e  
                on t.object_id = e.object_id  
                          where OBJECT_NAME(t.parent_id) = 'TEACHER' and 
                                                        e.type_desc = 'DELETE';  
INSERT INTO TEACHER
		VALUES('ASDF', 'ASDF FASD', 'м', 'ИСиТ');
DELETE TEACHER WHERE TEACHER.TEACHER = 'ASDF';
SELECT * FROM TR_ADULT
DROP TRIGGER TR_TEACHER_DEL1;
DROP TRIGGER TR_TEACHER_DEL2;
DROP TRIGGER TR_TEACHER_DEL3;
go --7
CREATE TRIGGER TR_TEACHER_TRAN ON TEACHER AFTER INSERT, DELETE, UPDATE
as DECLARE @tn varchar(100);
SET @tn = (SELECT TEACHER_NAME FROM inserted);
if (@tn = 'ASDF FASD')
begin 
	raiserror('Так нельзя', 10, 1);
	rollback;
end;
return;
go
INSERT INTO TEACHER
		VALUES('ASDF', 'ASDF FASD', 'м', 'ИСиТ');
go
SELECT TEACHER_NAME FROM TEACHER WHERE TEACHER = 'ASDF';
DROP TRIGGER TR_TEACHER_TRAN;
go --8
CREATE TRIGGER TR_TEACHER_INSTEAD_OF ON TEACHER INSTEAD OF DELETE
as raiserror('Удаление запрещено', 10, 1);
go
DELETE TEACHER WHERE TEACHER.PULPIT = 'ИСиТ';
SELECT * FROM TEACHER WHERE PULPIT = 'ИСиТ';
DROP TRIGGER TR_TEACHER_INSTEAD_OF;
go --9
create  trigger DDL_UNIVER on database 
                        for DROP_TABLE, CREATE_TABLE  as
	raiserror( N'Удаление и создание таблиц запрещено', 16, 1);  
	rollback;
go
CREATE TABLE TBL
(
	ID int identity
);
go
DROP TRIGGER DDL_UNIVER ON DATABASE;
DROP TABLE TR_ADULT;