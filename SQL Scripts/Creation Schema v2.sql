--USE master
--IF EXISTS(select * from sys.databases where name='sqltest')
--DROP DATABASE yourDBname
USE [master]
GO
/****** Object:  Database [sqltest]    Script Date: 13/01/2022 10:03:41 ******/
IF EXISTS(select * from sys.databases where name='sqltest')
BEGIN
	ALTER DATABASE [sqltest] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [sqltest]
END

USE master
CREATE DATABASE sqltest
GO

USE sqltest
--COLLATE SQL_Latin1_General_CP1_CI_AS --English USA
--Latin1_General_CI_AS italian

CREATE TABLE Account(
	Id int primary key identity,
	Email varchar(319), -- According to the standard 64 username 1 @ 254 domain name
	Password BINARY(64), -- To be hashed
	AccountType tinyint,
	IsDeleted Bit NOT NULL DEFAULT 0
)

CREATE TABLE tUser(
	Id int primary key identity,
	AccountId int,
	Name varchar(80),
	LastName varchar(80),
)

ALTER TABLE tUser
ADD CONSTRAINT User_AccountId
FOREIGN KEY (AccountId) REFERENCES Account(Id);

CREATE TABLE Brand(
	Id int primary key identity,
	AccountId int,
	BrandName varchar(100),
	Description nvarchar(MAX),
	IsDeleted Bit NOT NULL DEFAULT 0
)

ALTER TABLE Brand
ADD CONSTRAINT Brand_AccountId
FOREIGN KEY (AccountId) REFERENCES Account(Id);

CREATE TABLE Product(
	Id int primary key identity,
	BrandId int,
	Name varchar(80),
	ShortDescription nvarchar(MAX),
	Price decimal(18,2),
	Description nvarchar(MAX),
	IsDeleted Bit NOT NULL DEFAULT 0
)

ALTER TABLE Product
ADD CONSTRAINT Product_BrandId
FOREIGN KEY (BrandId) REFERENCES Brand(Id);
		
CREATE TABLE Category(
	Id int identity primary key,
	Name varchar(80)
)

CREATE TABLE Product_Category(
	ProductId int,
	CategoryId int,
	PRIMARY KEY(ProductId, CategoryId)
)

ALTER TABLE Product_Category
ADD CONSTRAINT ProductCategory_ProductId FOREIGN KEY (ProductId) REFERENCES Product(Id),
CONSTRAINT ProductCategory_CategoryId FOREIGN KEY (CategoryId) REFERENCES Category(Id);

CREATE TABLE Nation(
	Id int identity primary key,
	Name varchar(80),
)

CREATE TABLE InfoRequest(
	Id int primary key identity,
	UserId int,
	ProductId int,
	Name varchar(80),
	Lastname varchar(80),
	Email varchar(319),
	City varchar(80),
	NationId int,
	Phone char(15),
	PostalCode varchar(10),
	RequestText nvarchar(MAX),
	InsertDate Date,
	IsDeleted Bit NOT NULL DEFAULT 0
)

ALTER TABLE InfoRequest
ADD CONSTRAINT InfoRequest_UserId FOREIGN KEY (UserId) REFERENCES tUser(Id),
CONSTRAINT InfoRequest_ProductId FOREIGN KEY (ProductId) REFERENCES Product(Id),
CONSTRAINT InfoRequest_NationId FOREIGN KEY (NationId) REFERENCES Nation(Id),
CONSTRAINT chk_phone CHECK (Phone not like '%[^0-9]%');

CREATE TABLE InfoRequestReply(
	Id int identity primary key,
	InfoRequestId int,
	AccountId int,
	ReplyText nvarchar(MAX),
	InsertDate Date,
	IsDeleted Bit NOT NULL DEFAULT 0
)

ALTER TABLE InfoRequestReply
ADD CONSTRAINT InfoRequestReply_InfoRequestId FOREIGN KEY (InfoRequestId) REFERENCES InfoRequest(Id),
CONSTRAINT InfoRequestReply_AccountId FOREIGN KEY (AccountId) REFERENCES Account(Id);

--CREATE NONCLUSTERED INDEX IX_Price ON Product (Price)
--CREATE NONCLUSTERED INDEX IX_BrandId ON Product (BrandId)
--CREATE NONCLUSTERED INDEX IX_InfoRequestId On InfoRequestReply (InfoRequestId) INCLUDE (AccountId, ReplyText, InsertDate)
--CREATE NONCLUSTERED INDEX IX_ProductId ON InfoRequest (ProductId)