create database Flat;

use Flat;

CREATE TABLE Flats (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) not null,
    Square int check(Square > 0),
    BuildYear int check(BuildYear > 1900),
    Floor int,
	Price int check(Price > 0),
	IsBricked bit, 
	Image VARBINARY(MAX),
)


CREATE TABLE Addresses (
    ID INT PRIMARY KEY IDENTITY(1,1),
	FlatID INT FOREIGN KEY (FlatID) References Flats(ID),
    City VARCHAR(50),
	Street VARCHAR(20),
    House VARCHAR(20),
)
go
CREATE VIEW FlatsWithAddresses
AS
SELECT f.ID, f.Name, f.Square, BuildYear, Floor, Price, IsBricked, Image, l.ID as AddressId, l.City as LectorName, l.Street, l.House
FROM Flats f
INNER JOIN Addresses l
ON l.FlatID = f.ID;
go
CREATE TRIGGER tr_Flats_Addresses
ON Flats
AFTER UPDATE
AS
BEGIN
    IF UPDATE(Price)
    BEGIN
        UPDATE Flats
        SET Square = i.Price * 10
        FROM inserted i
        INNER JOIN Flats f ON i.ID = f.ID
    END
END




-- Создание хранимой процедуры для добавления нового продукта
CREATE PROCEDURE AddFlat
    @Name VARCHAR(50),
    @Semester int,
    @Kurs int,
    @LectionsNum int,
	@LabsNum int,
	@IsExam bit, 
	@Image VARBINARY(MAX)
AS
BEGIN
INSERT INTO Flats(Name, Square, BuildYear, Floor, Price, IsBricked, Image)
VALUES (@Name, @Semester, @Kurs, @LectionsNum, @LabsNum, @IsExam, @Image)
END

-- Создание хранимой процедуры для добавления нового пользователя
CREATE PROCEDURE AddAddress
	@DisciplineID INT,
    @Name VARCHAR(50),
	@Department VARCHAR(20),
    @Auditory VARCHAR(10)
AS
BEGIN
INSERT INTO Addresses(FlatID, City, Street, House)
VALUES (@DisciplineID, @Name, @Department, @Auditory)
END

Create PROCEDURE AddFlatWithAddress
	@Name VARCHAR(50),
    @Semester int,
    @Kurs int,
    @LectionsNum int,
	@LabsNum int,
	@IsExam bit, 
	@Image VARBINARY(MAX),
	@LectorName VARCHAR(50),
	@Department VARCHAR(20),
    @Auditory VARCHAR(10)
AS
BEGIN
EXEC AddFlat @Name, @Semester, @Kurs, @LectionsNum, @LabsNum, @IsExam, @Image;
DECLARE @IdentityValue INT;
SET @IdentityValue = IDENT_CURRENT('Flats');
SELECT @IdentityValue;
EXEC AddAddress @IdentityValue, @LectorName, @Department, @Auditory
END

select * from Lectors;
select * from Disciplines;