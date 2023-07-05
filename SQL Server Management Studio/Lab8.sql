DECLARE @ch char = 'fff',
		@vch varchar(5) = '����',
		@dtm datetime,
		@tm time = getdate(),
		@i int,
		@smi smallint,
		@ti tinyint,
		@num numeric(12, 5);
USE ��_MyBase;
SET @dtm = (select top(1) ����_������
			from ��������);
SET @tm = GETDATE();
SELECT @smi = 1, @ti = 42, @num = 5334.32421; 
SELECT @ch, @vch, @dtm, @tm, @i, @smi, @ti, @num; 

USE UNIVER;

DECLARE @cap int = (select SUM(AUDITORIUM.AUDITORIUM_CAPACITY) FROM AUDITORIUM)
IF @cap >= 200
begin
	DECLARE @av int = (SELECT AVG(AUDITORIUM.AUDITORIUM_CAPACITY) FROM AUDITORIUM)
	SELECT COUNT(*) '���������� ���������', AVG(AUDITORIUM_CAPACITY) '������� �����������', (select COUNT(*) from AUDITORIUM where AUDITORIUM_CAPACITY < @av) '���������, ������ �������'															
	FROM AUDITORIUM;
end
ELSE IF @cap < 200 print '����� ����������� ��������� �����' + cast(@cap as varchar(10));

SELECT @@ROWCOUNT '@@ROWCOUNT', @@VERSION '@@VERSION', @@SPID '@@SPID', @@ERROR '@@ERROR',
		@@SERVERNAME '@@SERVERNAME', @@TRANCOUNT '@@TRANCOUNT', @@FETCH_STATUS '@@FETCH_STATUS', @@NESTLEVEL '@@NESTLEVEL'

DECLARE @x float = 3,
		@t float = 3,
		@z float;
IF @t > @x
SET @z = power(sin(@t), 2);
ELSE IF @t < @x
SET @z = 4*(@t + @x);
ELSE IF @t = @x
SET @z = 1 - EXP(@x - 2);
print @z;

DECLARE @result nvarchar(30);
DECLARE @surname nvarchar(20) = '���������',
		@name nvarchar(20) = 'Ը���',
		@secondname nvarchar(20) = '�������������';
SET @result = CONCAT(@surname, ' ', left(@name, 1), '. ' , left(@secondname, 1) + '.');
PRINT @result;

SELECT YEAR(GETDATE()) - YEAR(STUDENT.BDAY)
FROM STUDENT
WHERE MONTH(STUDENT.BDAY) - MONTH(GETDATE()) = 1;

DECLARE @GROUP int = 5;

SELECT DATEPART(WEEKDAY, PROGRESS.PDATE)
FROM GROUPS INNER JOIN STUDENT ON STUDENT.IDGROUP = GROUPS.IDGROUP
INNER JOIN PROGRESS ON PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
WHERE GROUPS.IDGROUP = @GROUP AND PROGRESS.SUBJECT = '����'
GROUP BY DATEPART(WEEKDAY, PROGRESS.PDATE);

DECLARE @mark int = (select AVG(PROGRESS.NOTE) from PROGRESS);
IF @mark > 7
print '�������� �������� �������'
ELSE 
print '���������� ���� ���������';

SELECT COUNT(*), CASE 
		WHEN NOTE between 8 and 10 then '�����������!'
		WHEN NOTE between 6 and 7 then '�������'
		WHEN NOTE between 4 and 5 then '�����'
		WHEN NOTE between 1 and 3 then '�� ��������'
		else '���� ���� �� �����'
		end
FROM PROGRESS INNER JOIN STUDENT ON STUDENT.IDSTUDENT = PROGRESS.IDSTUDENT
INNER JOIN GROUPS ON STUDENT.IDGROUP = GROUPS.IDGROUP
WHERE GROUPS.FACULTY = '��'
GROUP BY CASE 
		WHEN NOTE between 8 and 10 then '�����������!'
		WHEN NOTE between 6 and 7 then '�������'
		WHEN NOTE between 4 and 5 then '�����'
		WHEN NOTE between 1 and 3 then '�� ��������'
		else '���� ���� �� �����'
		end;

DECLARE @tbl TABLE(
				INTS int primary key identity(1,1),
				CHARS char default 'a',
				TOOINTS int);
SET @i = 0;
WHILE @i < 10
begin
	INSERT @tbl(TOOINTS) 
	VALUES(floor(rand()*10000));
	SET @i = @i + 1
end;

SELECT *
FROM @tbl;

begin TRY
	INSERT INTO AUDITORIUM VALUES(NULL, NULL, NULL, NULL)
end TRY
begin CATCH
	print ERROR_NUMBER()
	print ERROR_MESSAGE()
	print ERROR_LINE()
	print ERROR_PROCEDURE()
	print ERROR_SEVERITY()
	print ERROR_STATE()
end CATCH

WHILE @i < 15
begin
	IF @i = 13
	RETURN
	print @i
	SET @i = @i + 1
end;