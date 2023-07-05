select * from test_orcl;

create table test_pdb(
    id number(3),
    text varchar2(20)
);

insert into test_orcl (id, text)
    values (100, 'sometext');
    
SELECT * FROM USER_CATALOG;