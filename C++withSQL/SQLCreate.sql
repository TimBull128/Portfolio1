USE [ExampleDB]
GO

CREATE TABLE [dbo].[TestTable](
	[ID] [int] NOT NULL,
	[FirstName] [varchar](max) NULL,
	[SecondName] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)


