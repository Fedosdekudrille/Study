CREATE VIEW ����������_���������([��� ���������], [������������ ���������], [��� ���������])
	as SELECT AUDITORIUM, AUDITORIUM_NAME, AUDITORIUM.AUDITORIUM_TYPE
	FROM AUDITORIUM
	WHERE AUDITORIUM_TYPE Like '��%' WITH CHECK OPTION
	go
	SELECT * from ����������_���������;