CREATE VIEW ����������(���, [������������ ����������], [��� �������])
	as SELECT TOP 1000 SUBJECT ���, SUBJECT_NAME [������������ ����������], PULPIT [��� �������]
	FROM SUBJECT
	ORDER BY SUBJECT_NAME
	go
	SELECT * FROM ����������;