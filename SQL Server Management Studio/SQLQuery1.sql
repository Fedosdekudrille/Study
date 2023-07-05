USE master;
CREATE database Ку_MyBase on primary
( name = N'AUTOMOBILE_mdf', filename = N'C:\STUDY\BD\AUTOMOBILE_mdf.mdf', 
   size = 10240Kb, maxsize=UNLIMITED, filegrowth=1024Kb),
( name = N'AUTOMOBILE_ndf', filename = N'C:\STUDY\BD\AUTOMOBILE_ndf.ndf', 
   size = 10240KB, maxsize=1Gb, filegrowth=25%),
filegroup FG1
( name = N'AUTOMOBILE_fg1_1', filename = N'C:\STUDY\BD\AUTOMOBILE_fgq-1.ndf', 
   size = 10240Kb, maxsize=1Gb, filegrowth=25%),
( name = N'AUTOMOBILE_fg1_2', filename = N'C:\STUDY\BD\AUTOMOBILE_fgq-2.ndf', 
   size = 10240Kb, maxsize=1Gb, filegrowth=25%)
log on
( name = N'AUTOMOBILE_log', filename=N'C:\STUDY\BD\AUTOMOBILE_log.ldf',       
   size=10240Kb,  maxsize=2048Gb, filegrowth=10%);
USE Ку_MyBase;
CREATE table ДЕТАЛИ
(	Артикул nvarchar(10) primary key,
	Название_детали nvarchar(20),
	Цена int,
	Примечание nvarchar(50),
	Количество int,
) on FG1;
CREATE table ЗАКАЗЧИКИ
(	Код_поставщика int primary key,
	Название_фирмы nvarchar(20),
	Адрес nvarchar(50),
	Телефон nvarchar(30),
) on FG1;
CREATE table ПОСТАВКИ
(	Номер_поставки int primary key,
	Код_поставщика int foreign key references ЗАКАЗЧИКИ(Код_поставщика),
	Артикул_детали nvarchar(10) foreign key references ДЕТАЛИ(Артикул),
	Количество int,
	Дата_заказа date,
) on FG1;
ALTER table ДЕТАЛИ ADD В_наличии nchar(1) default 'y'
check(В_наличии in ('y','n'));	
ALTER table ДЕТАЛИ ADD Лишняя_колонка int;
ALTER table ДЕТАЛИ DROP Column Лишняя_колонка;
INSERT into ДЕТАЛИ(Артикул, Название_детали, Цена, Примечание, Количество) 
	Values ('28D', 'Диски', 100,	'Колёсные дики, Сталь', 54),
			('34P',	'Поршень',	12,	'Поршень для двигателя',	23),
			('76A',	'Амортизатор',	50,	'Амортизатор, легковой(10т)',	61),
			('87F',	'Фары',	84,	'Фары, ближний и дальний свет',	25),
			('99K',	'Колёса',	220,	'Шины, камеры, зима, лето',	101);
INSERT into ЗАКАЗЧИКИ(Код_поставщика, Название_фирмы, Адрес, Телефон)
	Values (10,	'БелМеталл',	'Куйбышева, 31',	375253232542),
			(11,	'Motors',	'Проспект Независимости, 5',	375293672102),
			(12,	'Машинки',	'Нёманская, 2',	375258233205),
			(13,	'AllForCar',	'Орлова, 34',	375293242541),
			(14,	'Машинная',	'Купаловская, 83',	375448422495);
INSERT into ПОСТАВКИ(Номер_поставки, Код_поставщика, Артикул_детали, Количество, Дата_заказа)
	Values (3251,	10,	'28D',	15,	'2022-09-07'),
			(3252,	14,	'34P',	36,	'2022-09-10'),
			(3253,	12,	'99K',	30,	'2022-09-08'),
			(3254,	11,	'34P',	25,	'2022-09-08'),
			(3255,	13,	'87F',	50,	'2022-09-15');
SELECT Distinct Top(3) Название_детали [Дешёвые детали(топ 3)], Цена From ДЕТАЛИ
	Order by Цена;
SELECT count(*) [Количество видов деталей дешевле 100] From ДЕТАЛИ
	Where Цена < 100;
UPDATE ДЕТАЛИ set Цена = Цена + 5 Where Артикул = '34P';
SELECT * From ДЕТАЛИ;
INSERT into ДЕТАЛИ(Артикул) Values ('24T');
DELETE from ДЕТАЛИ Where Артикул = '24T';
SELECT Distinct Номер_поставки, Дата_заказа FROM ПОСТАВКИ Where Дата_заказа Between '2022-09-08'And'2022-09-10';
SELECT Название_детали FROM ДЕТАЛИ Where Название_детали Like '%О%';
SELECT Distinct Название_детали FROM ДЕТАЛИ Where Цена In (17, 84);