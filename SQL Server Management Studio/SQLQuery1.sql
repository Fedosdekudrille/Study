USE master;
CREATE database ��_MyBase on primary
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
USE ��_MyBase;
CREATE table ������
(	������� nvarchar(10) primary key,
	��������_������ nvarchar(20),
	���� int,
	���������� nvarchar(50),
	���������� int,
) on FG1;
CREATE table ���������
(	���_���������� int primary key,
	��������_����� nvarchar(20),
	����� nvarchar(50),
	������� nvarchar(30),
) on FG1;
CREATE table ��������
(	�����_�������� int primary key,
	���_���������� int foreign key references ���������(���_����������),
	�������_������ nvarchar(10) foreign key references ������(�������),
	���������� int,
	����_������ date,
) on FG1;
ALTER table ������ ADD �_������� nchar(1) default 'y'
check(�_������� in ('y','n'));	
ALTER table ������ ADD ������_������� int;
ALTER table ������ DROP Column ������_�������;
INSERT into ������(�������, ��������_������, ����, ����������, ����������) 
	Values ('28D', '�����', 100,	'������� ����, �����', 54),
			('34P',	'�������',	12,	'������� ��� ���������',	23),
			('76A',	'�����������',	50,	'�����������, ��������(10�)',	61),
			('87F',	'����',	84,	'����, ������� � ������� ����',	25),
			('99K',	'�����',	220,	'����, ������, ����, ����',	101);
INSERT into ���������(���_����������, ��������_�����, �����, �������)
	Values (10,	'���������',	'���������, 31',	375253232542),
			(11,	'Motors',	'�������� �������������, 5',	375293672102),
			(12,	'�������',	'͸�������, 2',	375258233205),
			(13,	'AllForCar',	'������, 34',	375293242541),
			(14,	'��������',	'�����������, 83',	375448422495);
INSERT into ��������(�����_��������, ���_����������, �������_������, ����������, ����_������)
	Values (3251,	10,	'28D',	15,	'2022-09-07'),
			(3252,	14,	'34P',	36,	'2022-09-10'),
			(3253,	12,	'99K',	30,	'2022-09-08'),
			(3254,	11,	'34P',	25,	'2022-09-08'),
			(3255,	13,	'87F',	50,	'2022-09-15');
SELECT Distinct Top(3) ��������_������ [������� ������(��� 3)], ���� From ������
	Order by ����;
SELECT count(*) [���������� ����� ������� ������� 100] From ������
	Where ���� < 100;
UPDATE ������ set ���� = ���� + 5 Where ������� = '34P';
SELECT * From ������;
INSERT into ������(�������) Values ('24T');
DELETE from ������ Where ������� = '24T';
SELECT Distinct �����_��������, ����_������ FROM �������� Where ����_������ Between '2022-09-08'And'2022-09-10';
SELECT ��������_������ FROM ������ Where ��������_������ Like '%�%';
SELECT Distinct ��������_������ FROM ������ Where ���� In (17, 84);