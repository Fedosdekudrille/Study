use FlatRecovery;

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