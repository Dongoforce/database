USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'WitcherDataBase'
) 
BEGIN 
	CREATE DATABASE WitcherDataBase
END
GO

USE WitcherDataBase
GO

IF OBJECT_ID('dbo.Order') IS NOT NULL
DROP TABLE dbo.[Order]
GO

IF OBJECT_ID('dbo.Monster') IS NOT NULL
DROP TABLE dbo.[Monster]
GO

IF OBJECT_ID('dbo.Witcher') IS NOT NULL
DROP TABLE dbo.[Witcher]
GO

IF OBJECT_ID('dbo.Susceptibility') IS NOT NULL
DROP TABLE dbo.[Susceptibility]
GO

CREATE TABLE dbo.[Susceptibility]
(
	Id	INT NOT NULL PRIMARY KEY IDENTITY(1,1),  
	[Name] NVARCHAR(50) NOT NULL,
	[Type] NVARCHAR(20) NOT NULL
);
GO

CREATE TABLE dbo.[Witcher]
(
	Id	INT NOT NULL PRIMARY KEY IDENTITY(1,1),  
	[Name] NVARCHAR(50) NOT NULL,
	[SkillLevel] NVARCHAR(20) NOT NULL,
	NumberOfKills INT NOT NULL
);
GO

CREATE TABLE dbo.[Monster]
(
	Id	INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(50) NOT NULL,
    [ThreatLevel] NVARCHAR(20) NOT NULL, 
    [Class] NVARCHAR(20) NOT NULL,
	SusceptibilityId INT NOT NULL REFERENCES dbo.[Susceptibility] (Id)
);
GO

CREATE TABLE dbo.[Order]
(
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    WitcherId INT NOT NULL REFERENCES dbo.[Witcher] (Id),
	MonsterId INT NOT NULL REFERENCES dbo.[Monster] (Id),
	CountOfMoney INT NOT NULL CHECK(CountOfMoney BETWEEN 1 AND 10000)
);
GO

/*
DROP TABLE dbo.[Monster]
DROP TABLE dbo.[Witcher]
DROP TABLE dbo.[Susceptibility]
DROP TABLE dbo.[Order]
*/

