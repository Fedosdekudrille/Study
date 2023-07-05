USE UNIVER;
go
ALTER FUNCTION COUNT_STUDENTS(@faculty varchar(20) = null, @prof varchar(20) = null) returns int
as
begin
	DECLARE @rc int = (SELECT COUNT(*) FROM STUDENT 
	INNER JOIN GROUPS ON STUDENT.IDGROUP = GROUPS.IDGROUP
	INNER JOIN FACULTY ON GROUPS.FACULTY = FACULTY.FACULTY
	WHERE FACULTY.FACULTY = @faculty AND GROUPS.PROFESSION = @prof);
	return @rc;
end;
go
SELECT dbo.COUNT_STUDENTS('ИТ', '1-40 01 02');
go --2
ALTER FUNCTION FSUBJECTS(@p varchar(20)) returns char(300)
as
begin
	DECLARE ZkSubj CURSOR LOCAL for 
			SELECT SUBJECT.SUBJECT FROM SUBJECT WHERE SUBJECT.PULPIT = @p;
	OPEN ZkSubj
	DECLARE @substr varchar(20), @str char(300) = '';
	FETCH ZkSubj into @substr;
	while @@FETCH_STATUS = 0
	begin
		SET @str = rtrim(@substr) + ', ' + @str;
		FETCH ZkSubj into @substr;
	end;
	return @str;
end;
go
SELECT SUBJECT.PULPIT, dbo.FSUBJECTS(SUBJECT.PULPIT)
FROM SUBJECT
GROUP BY SUBJECT.PULPIT;
go
CREATE FUNCTION FFACPUL(@f char(10), @p char(20)) returns table
as return SELECT FACULTY.FACULTY, PULPIT.PULPIT
		FROM FACULTY LEFT OUTER JOIN PULPIT
		ON FACULTY.FACULTY = PULPIT.FACULTY
		WHERE FACULTY.FACULTY = ISNULL(@f, FACULTY.FACULTY)
		AND PULPIT.PULPIT = ISNULL(@p, PULPIT.PULPIT);
go
   select * from dbo.FFACPUL(NULL, NULL);
   select * from dbo.FFACPUL('ЛХФ', NULL);
   select * from dbo.FFACPUL(NULL, 'ИСиТ');
   select * from dbo.FFACPUL('ЛХФ', 'ЛВ');
go
ALTER FUNCTION FCTEACHER(@p char(20)) returns int
as
begin
	DECLARE @num int = (SELECT COUNT(TEACHER.PULPIT)
	FROM TEACHER
	WHERE TEACHER.PULPIT = ISNULL(@p, TEACHER.PULPIT));
	return @num;
end;
go
   select dbo.FCTEACHER(NULL) 'Общее кол-во учителей';
   select PULPIT, dbo.FCTEACHER(PULPIT) 'Кол-во учителей' FROM PULPIT;
go
      create function FACULTY_REPORT(@c int) returns @fr table
                        ( [Факультет] varchar(50), [Количество кафедр] int, [Количество групп]  int, 
                                                                 [Количество студентов] int, [Количество специальностей] int )
as begin 
             declare cc CURSOR static for 
       select FACULTY from FACULTY 
                                                where dbo.COUNT_STUDENTS(FACULTY, default) > @c; 
       declare @f varchar(30);
       open cc;  
             fetch cc into @f;
       while @@fetch_status = 0
       begin
            insert @fr values( @f,  (select count(PULPIT) from PULPIT where FACULTY = @f),
            (select count(IDGROUP) from GROUPS where FACULTY = @f),   dbo.COUNT_STUDENTS(@f, default),
            (select count(PROFESSION) from PROFESSION where FACULTY = @f)   ); 
            fetch cc into @f;  
       end;   
             return; 
end;
go
SELECT dbo.FACULTY_REPORT(1);