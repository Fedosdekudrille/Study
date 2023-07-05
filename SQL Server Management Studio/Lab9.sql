use UNIVER
--1
exec SP_HELPINDEX 'AUDITORIUM_TYPE'
DROP TABLE #TEMPORARY

CREATE TABLE #TEMPORARY(
	TINDEX int,
	FIELD varchar(100));

SET nocount on;
DECLARE @i int = 0;
WHILE @i < 10000
	begin
		INSERT #TEMPORARY
		values(FLOOR(10000 * Rand()), REPLICATE('a', 2));
		SET @i = @i + 1;
	end;
SELECT *
FROM #TEMPORARY
where TINDEX between 700 and 1250
order by TINDEX;

 checkpoint;  --фиксация БД
 DBCC DROPCLEANBUFFERS;  --очистить буферный кэш

 CREATE clustered index #TEMPORARY_CL on #TEMPORARY(TINDEX asc)

 SELECT *
FROM #TEMPORARY
where TINDEX between 700 and 1250
order by TINDEX;

--2
DROP TABLE #NON_CL
CREATE TABLE #NON_CL
(    TKEY int, 
      IDT int identity(1, 1),
      ST varchar(100),
);

SET nocount on;           
DECLARE @k int = 0;
WHILE   @k < 20000       -- добавление в таблицу 20000 строк
begin
    INSERT #NON_CL(TKEY, ST)
	Values(floor(3000*RAND()), replicate('строка ', 10));
    SET @k = @k + 1; 
end;

CREATE index #NON_NONCLU on #NON_CL(TKEY, IDT);

SELECT * FROM  #NON_CL
WHERE  TKEY > 1500 and  IDT < 4500
ORDER BY  TKEY, IDT

SELECT *
FROM  #NON_CL
WHERE  TKEY = 56 and  IDT > 3

--3

CREATE  index #NON_CL_COVER on #NON_CL(TKEY) INCLUDE (IDT);
SELECT IDT from #NON_CL where TKEY>1500;

--4

SELECT TKEY
FROM #NON_CL
WHERE TKEY>=1500 and TKEY < 2000; --0.013

CREATE  index #NON_CL_WHERE on #NON_CL(TKEY) where (TKEY>=1500 and --0.007
TKEY < 2000);  

--5
CREATE INDEX #NON_CL_TKEY ON #NON_CL(TKEY);
INSERT top(1000000) #NON_CL(TKEY, ST) select TKEY, ST from #NON_CL;

ALTER index #NON_CL_TKEY on #NON_CL reorganize;
ALTER index #NON_CL_TKEY on #NON_CL rebuild with (online = off);

--6

DROP index #NON_CL_TKEY on #NON_CL;
CREATE index #NON_CL_TKEY on #NON_CL(TKEY) with (fillfactor = 65);
INSERT top(100) #NON_CL(TKEY, ST) select TKEY, ST from #NON_CL;