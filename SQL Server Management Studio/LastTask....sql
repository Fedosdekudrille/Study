ALTER FUNCTION PULPIT_COUNT(@f varchar(30)) returns int
as
begin
	DECLARE @i int = (select count(PULPIT) from PULPIT where FACULTY = @f);
	return @i;
end;
go
ALTER FUNCTION [dbo].[COUNT_STUDENTS](@faculty varchar(20) = null) returns int
as
begin
	DECLARE @rc int = (SELECT COUNT(*) FROM STUDENT 
	INNER JOIN GROUPS ON STUDENT.IDGROUP = GROUPS.IDGROUP
	INNER JOIN FACULTY ON GROUPS.FACULTY = FACULTY.FACULTY
	WHERE FACULTY.FACULTY = @faculty);
	return @rc;
end;
go
ALTER FUNCTION GROUP_COUNT(@f varchar(30)) returns int
as
begin
	DECLARE @i int =(select count(IDGROUP) from GROUPS where FACULTY = @f);
	return @i;
end;
go
ALTER FUNCTION PROFESSION_COUNT(@f varchar(30)) returns int
as
begin
	DECLARE @i int =(select count(PROFESSION) from PROFESSION where FACULTY = @f);
	return @i;
end;
go
alter function FACULTY_REPORT(@c int) returns @fr table
	( [Факультет] varchar(50), [Количество кафедр] int, [Количество групп]  int, 
	                                         [Количество студентов] int, [Количество специальностей] int )
as begin 
	      declare cc CURSOR static for 
	select FACULTY from FACULTY where dbo.COUNT_STUDENTS(FACULTY) > @c; 
	declare @f varchar(30);
	open cc; 
	fetch cc into @f;
	while @@fetch_status = 0
	begin
	     insert @fr values( @f,  dbo.PULPIT_COUNT(@f),
	     dbo.GROUP_COUNT(@f),   dbo.COUNT_STUDENTS(@f),
	     dbo.PROFESSION_COUNT(@f)   ); 
	     fetch cc into @f;  
	end;   
	return; 
end;
go
SELECT * FROM dbo.FACULTY_REPORT(0);