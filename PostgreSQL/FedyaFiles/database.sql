create table users
(
	Id serial primary key,
	Name varchar(50) check(Name !='') not null,
	Register_date timestamp default CURRENT_TIMESTAMP not null,
	Email varchar(50) unique check(Email !='') not null,
	Password varchar(50) not null check(length(password) > 3),
	Admin boolean not null default false,
	Icon varchar(100) check(Icon !='')
);
alter table users add column likes int not null default 0
create table p_decoration_types
(
	Id serial primary key,
	Type varchar(50) unique check(Type !='') not null 
);
insert into p_decoration_types(type)
values('background')

select * from user_decorations
create  table profile_decoration 
(
	Id serial primary key,
	Type varchar(50) not null CONSTRAINT fk_p_decoration_types_profile_decoration references p_decoration_types(type),
	Name varchar(50) check(Name !='') not null,
	Picture varchar(50) check(Picture !='') not null,
	Cost decimal not null
);

insert into profile_decoration(type, name, picture, cost)
values('background', 'greenBg', '/', 3.5 )

create table user_decorations
(
	id serial primary key,
	decoration_id int not null CONSTRAINT fk_u_dec_prof_dec references profile_decoration(Id),
	user_id int not null CONSTRAINT fk_users_u_dec references users(Id),
	is_selected bool not null default false
)

create table threads 
(
	Id serial primary key,
	Creator_id int check(Creator_id !=0) not null CONSTRAINT fk_threads_creator references users(Id),
	Creation_date timestamp default CURRENT_TIMESTAMP not null,
	Name varchar(50) check(Name !='') not null
);

create table messages
(
	Id serial primary key,
	Thread_id bigint not null CONSTRAINT fk_message_thread references threads(Id), 
	User_id bigint not null check(User_id !=0) CONSTRAINT fk_message_user references users(Id),
	Content varchar(50) check(Content !='') not null,
	Picture varchar(100) check(Picture !='') not null,
	Creation_date timestamp not null default CURRENT_TIMESTAMP
);

create table messages_likes
(
	Id serial primary key,
	Message_id bigint not null CONSTRAINT fk_messages_likes_messge references messages(Id),
	User_id bigint not null CONSTRAINT fk_messages_likes_user references users(Id)
);

create table topics
(
	Id serial primary key,
	Name varchar(100) check(Name !='') unique not null
);

insert into topics(name) values('spells')

create table article
(
    Id serial primary key,
	Theme varchar(100) not null CONSTRAINT fk_article_theme references topics(Name),
	User_id bigint not null CONSTRAINT fk_article_user references users(Id),
	Content varchar(10000) check(Content !='') not null,
	Picture varchar(100) check(Picture !=''),
	Creation_date timestamp default CURRENT_TIMESTAMP not null,
	is_confirmed boolean not null default false,
	textsearchable_index_col tsvector
);
ALTER TABLE article ADD COLUMN name varchar(100) check(name != '') not null;
CREATE INDEX textsearch_idx ON article USING GIN (textsearchable_index_col);
CREATE OR REPLACE FUNCTION update_textsearchable_index_col()
RETURNS TRIGGER AS $$
BEGIN
   NEW.textsearchable_index_col :=
      setweight(to_tsvector('english', coalesce(NEW.name,'')), 'A') ||
      setweight(to_tsvector('english', coalesce(NEW.theme,'')), 'B') ||
      setweight(to_tsvector('english', coalesce(NEW.content,'')), 'D');
   RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE TRIGGER trigger_update_textsearchable_index_col
BEFORE INSERT OR UPDATE ON article
FOR EACH ROW
EXECUTE FUNCTION update_textsearchable_index_col();

create table article_likes
(
	Id serial primary key,
	Article_id bigint not null CONSTRAINT fk_article_likes_article references article(Id),
	User_id bigint not null	CONSTRAINT fk_article_likes_user references users(Id)
);