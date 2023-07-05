use UNIVER

DECLARE ZkSubject CURSOR GLOBAL
		for SELECT SUBJECT FROM SUBJECT WHERE PULPIT = 'ИСиТ';
OPEN ZkSubject;
DECLARE @substr char(20), @str char(300) = '';
while 1 = 1 
begin
	FETCH ZkSubject into @substr;
	IF @@FETCH_STATUS != 0
	BREAK;
	SET @str = rtrim(@substr) + ', ' + @str;
end
print 'Дисциплины ИСиТ:'
print @str;
CLOSE ZkSubject;

--2

DECLARE ZkLocal CURSOR Local
		for SELECT SUBJECT FROM SUBJECT WHERE PULPIT = 'ИСиТ';
OPEN ZkLocal;
FETCH ZkLocal into @substr;
print @substr;
go
DECLARE @substr char(20), @str char(300) = '';
FETCH ZkLocal into @substr;
print @substr;

DECLARE ZkGlobal CURSOR GLOBAL
		for SELECT SUBJECT FROM SUBJECT WHERE PULPIT = 'ИСиТ';
go
DECLARE @substr char(20), @str char(300) = '';
OPEN ZkGlobal;
FETCH ZkGlobal into @substr;
print @substr;
go
DECLARE @substr char(20), @str char(300) = '';
FETCH ZkGlobal into @substr;
print @substr;
deallocate ZkGlobal

--3

  DECLARE @num char(10), @type char(5);  
	DECLARE Zakaz CURSOR LOCAL STATIC                              
		 for SELECT AUDITORIUM, AUDITORIUM_TYPE
		       FROM AUDITORIUM where AUDITORIUM_TYPE = 'ЛК';				   
	open Zakaz;
	print   'Количество строк : '+cast(@@CURSOR_ROWS as varchar(5));
	INSERT AUDITORIUM(AUDITORIUM, AUDITORIUM_TYPE) 
	                 values ('1000', 'ЛК'); 
	FETCH  Zakaz into @num, @type;     
	while @@fetch_status = 0                                    
      begin 
          print @num + ' '+ @type;      
          fetch Zakaz into @num, @type; 
       end;
	DELETE FROM AUDITORIUM WHERE AUDITORIUM = '1000'
   CLOSE  Zakaz;

--4
    DECLARE SBJ cursor local STATIC SCROLL            
          for SELECT SUBJECT
	   FROM SUBJECT WHERE PULPIT = 'ИСиТ'; 
	OPEN SBJ;
	FETCH SBJ into  @str;                 
	print 'следующая строка: ' + @str;      
	FETCH LAST from  SBJ into @str;       
	print 'последняя строка: ' + @str; 
	FETCH FIRST FROM SBJ into @str;
	print 'первая строка: ' + @str;
	FETCH NEXT FROM SBJ into @str;
	print 'следующая строка за текущей: ' + @str;
	FETCH PRIOR FROM SBJ into @str;
	print 'предыдущая строка от текущей: ' + @str;
	FETCH ABSOLUTE 5 FROM SBJ into @str;
	print 'пятая строка от начала: ' + @str;
	FETCH ABSOLUTE -3 FROM SBJ into @str;
	print 'третья строка от конца: ' + @str;
	FETCH RELATIVE -3 FROM SBJ into @str;
	print 'третья строка назад от текущей: ' + @str;
	FETCH RELATIVE 4 FROM SBJ into @str;
	print 'четвёртая строка вперед от текущей: ' + @str;
    CLOSE SBJ;

INSERT INTO PROGRESS(SUBJECT, NOTE)
			Values('ОАиП', 2),
				('ОАиП', 3),
				('КГ', 3);
DECLARE ProgressDelete CURSOR LOCAL DYNAMIC
		FOR SELECT NOTE FROM PROGRESS FOR UPDATE;
DECLARE @i int;
Open ProgressDelete;
while 1 = 1
begin
FETCH ProgressDelete into @i;
if @@FETCH_STATUS != 0
break;
if @i < 4
DELETE PROGRESS WHERE CURRENT OF ProgressDelete;
end

DECLARE StudentProgressUpgrade CURSOR LOCAL
			FOR SELECT IDSTUDENT FROM PROGRESS FOR UPDATE;
Open StudentProgressUpgrade;
while 1 = 1
begin
FETCH StudentProgressUpgrade into @i;
print @i;
if @@FETCH_STATUS != 0
break;
if @i = 1005
UPDATE PROGRESS SET NOTE = NOTE + 1 WHERE CURRENT OF StudentProgressUpgrade;
end