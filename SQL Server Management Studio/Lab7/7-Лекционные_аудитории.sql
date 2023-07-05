CREATE VIEW Лекционные_аудитории([Код аудитории], [Наименование аудитории], [Тип Аудитории])
	as SELECT AUDITORIUM, AUDITORIUM_NAME, AUDITORIUM.AUDITORIUM_TYPE
	FROM AUDITORIUM
	WHERE AUDITORIUM_TYPE Like 'ЛК%' WITH CHECK OPTION
	go
	SELECT * from Лекционные_аудитории;