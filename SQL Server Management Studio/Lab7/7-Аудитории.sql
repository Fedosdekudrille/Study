CREATE VIEW ���������([��� ���������], [������������ ���������], [��� ���������])
	as SELECT AUDITORIUM, AUDITORIUM_NAME, AUDITORIUM.AUDITORIUM_TYPE
	FROM AUDITORIUM
	WHERE AUDITORIUM_TYPE Like '��%'
	go
	SELECT * from ���������;