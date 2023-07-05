use University;

CREATE TABLE Disciplines (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) not null,
    Semester int check(Semester = 1 OR Semester = 2 OR Semester=12),
    Kurs int check(Kurs=1 OR Kurs=2 OR Kurs=3 OR Kurs=4),
    LectionsNum int check(LectionsNum > 0),
	LabsNum int check(LabsNum > 0),
	IsExam bit, 
	Image VARBINARY(MAX),
)


CREATE TABLE Lectors (
    ID INT PRIMARY KEY IDENTITY(1,1),
	DisciplineID INT FOREIGN KEY (DisciplineID) References Disciplines(ID),
    Name VARCHAR(50),
	Department VARCHAR(20),
    Auditory VARCHAR(10),
)
go
CREATE VIEW DisciplinesWithLectors
AS
SELECT d.ID, d.Name, Semester, Kurs, LectionsNum, LabsNum, IsExam, Image, l.ID as LectorID, l.Name as LectorName, l.Department, l.Auditory
FROM Disciplines d
INNER JOIN Lectors l
ON l.DisciplineID = d.ID;
go
CREATE TRIGGER tr_Disciplines_UpdateLabs
ON Disciplines
AFTER UPDATE
AS
BEGIN
    IF UPDATE(LabsNum)
    BEGIN
        UPDATE Disciplines
        SET LectionsNum = i.LabsNum * 2
        FROM inserted i
        INNER JOIN Disciplines p ON i.ID = p.ID
    END
END