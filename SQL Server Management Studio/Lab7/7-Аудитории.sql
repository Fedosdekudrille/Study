CREATE VIEW Аудитории([Код аудитории], [Наименование аудитории], [Тип Аудитории])
	as SELECT AUDITORIUM, AUDITORIUM_NAME, AUDITORIUM.AUDITORIUM_TYPE
	FROM AUDITORIUM
	WHERE AUDITORIUM_TYPE Like 'ЛК%'
	go
	SELECT * from Аудитории;