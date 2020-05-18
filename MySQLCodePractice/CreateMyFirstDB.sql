-- Create new database named MyFirstDatabase, at the filepath FILENAME
CREATE DATABASE MyFirstDatabase ON (
		NAME = MyDB_dat, 
		FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\\MyDB.mdf',
		SIZE = 10,
		MAXSIZE = 50,
		FILEGROWTH = 5
	)

	LOG ON (
		NAME = MyDB_log,
		FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\MyDBLog.ldf',
		SIZE = 5MB,
		MAXSIZE = 25MB, FILEGROWTH = 5MB
		);
--Use the database that was created
USE MyFirstDatabase
GO

-- Create new table called products --
CREATE TABLE dbo.Products1
(
	ProductID int NULL,
	ProductName varchar(20) NULL,
	UnitPrice money NULL,
	ProductDesc varchar(50) NULL
);




/*

-- Alter table - note if running all straight away no need use and go
--Add column 
USE MyFirstDatabase
GO
alter TABLE dbo.Products1
add InStock bit Null;

-- Alter column size
USE MyFirstDatabase
GO
alter TABLE dbo.Products1
alter Column ProductDesc varchar(100) Null;

--Delete column
USE MyFirstDatabase
GO
alter TABLE dbo.Products1
drop Column InStock ;

--Delete table
USE MyFirstDatabase
GO
drop TABLE dbo.Products1

--Delete database
USE MyFirstDatabase
GO
drop database MyFirstDatabase
*/