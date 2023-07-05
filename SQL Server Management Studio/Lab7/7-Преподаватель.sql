CREATE VIEW [Преподаватель]
	as SELECT TEACHER код, TEACHER_NAME [имя преподавателя], GENDER пол, PULPIT [код кафедры]
	FROM TEACHER;