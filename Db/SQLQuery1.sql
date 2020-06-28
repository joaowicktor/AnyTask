create table users(
	id int identity(1,1) not null primary key,
	name varchar(200) not null,
	email varchar(200) not null,
	password varchar(200)
)

create table tasks(
	id int identity(1,1) not null primary key,
	description varchar(200) not null,
	concluded bit default(0) not null,
	id_user int not null foreign key references dbo.users(id)
)


select * from users