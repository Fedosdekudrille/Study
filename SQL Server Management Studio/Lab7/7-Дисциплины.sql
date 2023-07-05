CREATE VIEW Дисциплины(код, [наименование дисциплины], [код кафедры])
	as SELECT TOP 1000 SUBJECT код, SUBJECT_NAME [наименование дисциплины], PULPIT [код кафедры]
	FROM SUBJECT
	ORDER BY SUBJECT_NAME
	go
	SELECT * FROM Дисциплины;