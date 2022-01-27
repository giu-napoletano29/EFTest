USE master
--IF EXISTS(select * from sys.databases where name='yourDBname')
--DROP DATABASE yourDBname

CREATE DATABASE sqltest

CREATE TABLE Account(
	Id int primary key identity,
	Email varchar(255),
	Password varchar(255),
	AccountType tinyint constraint ck_Account check (AccountType in (1,2))
)

CREATE TABLE tUser(
	Id int primary key identity,
	AccountId int FOREIGN KEY REFERENCES Account(Id),
	Name varchar(255),
	LastName varchar(255),
)

CREATE TABLE Brand(
	Id int primary key identity,
	AccountId int FOREIGN KEY REFERENCES Account(Id),
	BrandName varchar(255),
	Description varchar(255)
)

CREATE TABLE Prodotto(
	ProductId int primary key identity,
	BrandId int FOREIGN KEY REFERENCES Brand(Id),
	Nome varchar(255),
	ShortDescription varchar(255),
	Price decimal(18,2),
	Description varchar(255)
)
		
CREATE TABLE Category(
	Id int identity primary key,
	Name varchar(255)
)

CREATE TABLE Prodotto_Categoria(
	IdProdotto int FOREIGN KEY REFERENCES Prodotto(ProductId),
	IdCategoria int FOREIGN KEY REFERENCES Category(Id),
	PRIMARY KEY(IdProdotto, IdCategoria)
)

CREATE TABLE Nation(
	Id int identity primary key,
	Name varchar(255),
)

CREATE TABLE InfoRequest(
	Id int primary key identity,
	UserId int FOREIGN KEY REFERENCES tUser(Id),
	ProductId int FOREIGN KEY REFERENCES Prodotto(ProductId),
	Nome varchar(255),
	Cognome varchar(255),
	Email varchar(255),
	Città varchar(255),
	IdStato int FOREIGN KEY REFERENCES Nation(Id),
	Telefono char(15) CONSTRAINT chk_phone CHECK (Telefono not like '%[^0-9]%'),
	CAP varchar(10),
	TestoRichiesta varchar(255),
	InsertDate Date
)

CREATE TABLE InfoRequestReply(
	Id int identity primary key,
	InfoRequestId int FOREIGN KEY REFERENCES InfoRequest(Id),
	AccountId int FOREIGN KEY REFERENCES Account(Id),
	ReplyText varchar(255),
	InsertDate Date
)
