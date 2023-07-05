create table users
(
	Id serial primary key,
	Name varchar(50) check(Name !='') not null,
	Register_date timestamp default CURRENT_TIMESTAMP not null,
	Email varchar(50) unique check(Email !='') not null,
	Password varchar(50) not null check(length(password) > 3)
	Admin boolean not null default false,
	Icon varchar(100) check(Icon !=''),
	Icon_decoration_id bigint CONSTRAINT fk_user_icon_decoration references Profile_decoration(Id),
	Background_decoration_id bigint CONSTRAINT fk_user_background_decoration references Profile_decoration(Id)
);
create table profile_decoration
(
	Id serial primary key,
	Type int not null CONSTRAINT fk_p_decoration_types_profile_decoration references p_decoration_types(Id),
	Name varchar(50) check(Name !='') not null,
	Picture varchar(50) check(Picture !='') not null,
	Cost decimal not null
);

create table p_decoration_types
(
	Id serial primary key,
	Type varchar(50) unique check(Type !='') not null 
);

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


create table article
(
    Id serial primary key,
	Theme_id bigint not null CONSTRAINT fk_article_theme references topics(Id),
	User_id bigint not null CONSTRAINT fk_article_user references users(Id),
	Content varchar(10000) check(Content !='') not null,
	Picture varchar(100) check(Picture !=''),
	Creation_date timestamp default CURRENT_TIMESTAMP not null
);
ALTER TABLE article ADD COLUMN name varchar(100) check(name != '') not null;
ALTER TABLE article ADD COLUMN textsearchable_index_col tsvector;
UPDATE article SET textsearchable_index_col =
     setweight(to_tsvector('english', coalesce(name,'')), 'A') || setweight(to_tsvector('english', coalesce('D&D FAQ','')), 'B') || setweight(to_tsvector('english', coalesce(content,'')), 'D');
select * from article
CREATE INDEX textsearch_idx ON article USING GIN (textsearchable_index_col);

CREATE OR REPLACE FUNCTION update_textsearchable_index_col()
RETURNS TRIGGER AS $$
BEGIN
   NEW.textsearchable_index_col :=
      setweight(to_tsvector('english', coalesce(NEW.name,'')), 'A') ||
      setweight(to_tsvector('english', coalesce('D&D FAQ','')), 'B') ||
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

create table edited_articles
(
	Id serial primary key,
	User_id bigint not null CONSTRAINT fk_edited_articles_user references users(Id),
	Article_id bigint not null CONSTRAINT fk_edited_articles_article references article(Id),
	Awaiting_confirmation boolean not null default true,
	Creation_date timestamp not null,
	Picture varchar(100) check(Picture !='') not null
);