-------------------------------------------------------------------------------
USE [MpcWinFormsIoCDataBase]
GO

DECLARE @version varchar(50) = '004';
DECLARE @description varchar(50) = 'Create User Country Relationship';
DECLARE @author varchar(50) = 'Joao Carvalho';

INSERT INTO [dbo].[_DbMigrations]([Version],[Description],[Author],[MigrationDate])
  VALUES (@version,@description,@author,GETDATE())
GO
-------------------------------------------------------------------------------

ALTER TABLE [dbo].[Users]
    ADD [CountryID] [int]
GO

ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_Countries]
FOREIGN KEY([CountryID]) REFERENCES [dbo].[Countries]([id]);
