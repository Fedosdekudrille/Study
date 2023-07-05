create table users
(
   Id serial primary key,
   Name varchar(20) CHECK(Name !='') not null,
   Register_date timestamp  not null,
   Email varchar(50)  CHECK(Email !='') UNIQUE not null,
   Admin boolean default false,
   Games_history bigint  CONSTRAINT games_history_key UNIQUE,
   Games_featured bigint CONSTRAINT games_featured_key UNIQUE	
);

insert into users (Name, Register_date, Email) values	('Gigachad', CURRENT_DATE, 'random@yandex.ru');
select name from users;


create table featured
(
	Id serial primary key,
	User_id bigint  UNIQUE not null CONSTRAINT fk_user_featured references users (Id),
	Game_id bigint UNIQUE not null CONSTRAINT fk_game_featured references games (Id),
	Time timestamp not null
);

create table games 
(
	Id serial primary key,
	Token varchar(50) CHECK(Token !='') not null,
	Token_network varchar(50) CHECK(Token_network !=''),
	Game_genre varchar(50) CHECK(Game_genre !='') not null,
	Free_tier boolean default true,
	Min_bet decimal not null,
	Max_bet decimal not null
);

create table rooms
(
	Id serial primary key,
	Created_at timestamp not null,
	Created_by bigint not null CONSTRAINT fk_rooms_user references users (Id),
	Game_id bigint not null CONSTRAINT fk_rooms_game references games (Id)
);

create table room_users
(
	Id serial primary key,
	User_id bigint not null CONSTRAINT fk_room_users_user references users (Id),
	Connection_date timestamp not null,
	User_type varchar(20) not null,
	Room_id bigint not null CONSTRAINT fk_rooms_users_room references rooms (Id)
	
);

create table game_process
(
	Id serial primary key,
	Bet decimal not null,
	Creation_time timestamp not null,
	Finish_time timestamp, 
	Game_id bigint not null CONSTRAINT fk_game_process_game references games (Id),
	Room_id bigint not null CONSTRAINT fk_game_process_room references rooms (Id)
);

create table results 
(
	Id serial primary key,
	User_id bigint not null CONSTRAINT fk_results_user references users (Id),
	Balance_results money not null,
	Game_process_id bigint  not null CONSTRAINT fk_results_game_process references game_process (Id)
);

create table admins
(
	Id serial primary key,
	User_id bigint not null CONSTRAINT fk_admin_user references users (Id),
	Access_level smallint not null
);

create table moderation_logs
(
	Id serial primary key,
	Admin_id bigint not null CONSTRAINT fk_moderation_logs_admin references admins (Id),
	Action varchar(50) not null,
	Timme timestamp not null
);

create table messages 
(
	Id serial primary key,
	Sender_id bigint not null CONSTRAINT fk_messages_sender references users (Id),
	Resiever_id bigint not null CONSTRAINT fk_messages_resiever references users (Id),
	Game_id bigint not null CONSTRAINT fk_messages_game references games (Id),
	Content varchar(100) not null
);
