USE UNIVER;
go --1
ALTER PROCEDURE PSUBJECT
as
begin
	SELECT * FROM SUBJECT ORDER BY SUBJECT.SUBJECT;
	return (SELECT COUNT(*) FROM SUBJECT);
END
go
DECLARE @i INT;
EXEC @i = PSUBJECT;
print @i;
go --2
ALTER PROCEDURE PSUBJECT @p varchar(20), @c int output
as
begin
	SELECT * FROM SUBJECT WHERE SUBJECT.PULPIT = @p ORDER BY SUBJECT.SUBJECT;
	SET @c = (SELECT count(*) FROM SUBJECT WHERE SUBJECT.PULPIT = @p); 
	return (SELECT COUNT(*) FROM SUBJECT);
END
go
DECLARE @i INT, @r int = 0;
EXEC @i = PSUBJECT @p = 'ИСиТ', @c = @r output;
print cast(@i as varchar(3)) + ' ' + cast(@r as varchar(3));
go --3
ALTER PROCEDURE PSUBJECT @p varchar(20)
as	
	SELECT * FROM SUBJECT WHERE SUBJECT.PULPIT = @p ORDER BY SUBJECT.SUBJECT;
go
CREATE table #tbl
(
	[SUBJECT] [char](10) primary key,
	[SUBJECT_NAME] [varchar](100) NULL,
	[PULPIT] [char](20) NULL,
)
INSERT #tbl EXEC PSUBJECT @p = 'ИСиТ';
INSERT #tbl EXEC PSUBJECT @p = 'ОХ';
SELECT * FROM #tbl;
DROP table #tbl;
go --4
ALTER PROCEDURE PAUDITORIUM_INSERT @a CHAR(20), @n VARCHAR(50), @c int, @t CHAR(10)
as
begin try
	insert into AUDITORIUM
					values(@a, @n, @c, @t);
	return 1;
end try
begin catch
	print 'Номер ошибки: ' + cast(error_number() as varchar(6));
	print 'Сообщение: ' + error_message();
	print 'Уровень: ' + cast(error_severity() as varchar(6));
	print 'Метка: ' + cast(error_state() as varchar(8));
	print 'Номер строки: ' + cast(error_line() as varchar(8));
	if ERROR_PROCEDURE() is not null
	print 'Имя процедуры: ' + error_procedure();
	return -1;
end catch;
go
DECLARE @rc int;
EXEC @rc = PAUDITORIUM_INSERT '10000', 'Л', 5, '10000';
print @rc;
go --5
ALTER procedure SUBJECT_REPORT  @p CHAR(10)
as  
DECLARE @rc int = 0;                            
begin try    
    DECLARE @tv char(10), @t char(300) = ' '; 
    DECLARE ZkSub CURSOR local for 
    SELECT SUBJECT.SUBJECT from SUBJECT where PULPIT = @p;
    if not exists (SELECT SUBJECT.SUBJECT from SUBJECT where PULPIT = @p)
        raiserror('ошибка', 11, 1);
    else 
    open ZkSub;
	fetch  ZkSub into @tv;  
	print   'Заказанные товары';
	while @@fetch_status = 0                                   
	begin 
	   set @t = rtrim(@tv) + ', ' + @t; 
	   set @rc = @rc + 1;
	   fetch  ZkSub into @tv; 
	end 
	print @t;        
	close  ZkSub;
	return @rc;
end try  
begin catch              
   print 'ошибка в параметрах' 
   if error_procedure() is not null   
print 'имя процедуры : ' + error_procedure();
      return @rc;
end catch;
go
DECLARE @rc int;
EXEC @rc = SUBJECT_REPORT 'ИСиТ';
print @rc;
go --6
ALTER PROCEDURE PAUDITORIUM_INSERTX @a CHAR(20), @n VARCHAR(50), @c int, @t CHAR(10), @tn varchar(30)
as
begin try
set transaction isolation level SERIALIZABLE;          
    begin tran
	INSERT INTO AUDITORIUM_TYPE
					VALUES(@n, @tn);
	DECLARE @rc int;
	EXEC @rc = PAUDITORIUM_INSERT @a, @n, @c, @t;
	print @rc;
	commit tran;
	return 1;
end try
begin catch
	print 'Номер ошибки: ' + cast(error_number() as varchar(6));
	print 'Сообщение: ' + error_message();
	print 'Уровень: ' + cast(error_severity() as varchar(6));
	print 'Метка: ' + cast(error_state() as varchar(8));
	print 'Номер строки: ' + cast(error_line() as varchar(8));
	if ERROR_PROCEDURE() is not null
	print 'Имя процедуры: ' + error_procedure();
	return -1;
end catch;
go
DECLARE @rc int;
EXEC @rc = PAUDITORIUM_INSERTX '10000', 'Л', 5, '10000', 'Не сущ.';
print @rc;
DELETE FROM AUDITORIUM WHERE AUDITORIUM.AUDITORIUM_TYPE = 'Л';
DELETE FROM AUDITORIUM_TYPE WHERE AUDITORIUM_TYPE.AUDITORIUM_TYPE = 'Л';
