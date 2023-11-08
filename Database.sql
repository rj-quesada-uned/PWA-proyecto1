create database store;

use store;

create table Tools (
	ToolId int primary key,
	ToolName varchar(40),
	ToolDescription varchar(255),
	ToolState int,
	BorrowedDate date,

);

create table User (
	id int identity(1,1) primary key,
	PersonalId varchar(40),
	FirstName varchar(255),
	LastName  varchar(255),
	RegisterDate date,
	UserState int,
	BorrowedTool int foreign key references Tools(ToolId)
);

