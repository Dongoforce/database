USE WitcherDataBase
--Скалярная функция
GO

DROP FUNCTION IF EXISTS dbo.CountOfKills

GO
CREATE FUNCTION dbo.CountOfKills()
RETURNS INT
AS
BEGIN
RETURN(
	SELECT COUNT(MonsterId) AS countKills FROM dbo.[Order] JOIN dbo.Monster ON dbo.[Order].MonsterId = dbo.Monster.Id
	WHERE Monster.[Name] = 'Golem'
)
END
GO

SELECT dbo.CountOfKills() AS COUNTKILL

--Подставляемая табличная функция
GO

DROP FUNCTION IF EXISTS dbo.ExpensiveOrder

GO

CREATE FUNCTION dbo.ExpensiveOrder()
RETURNS TABLE
AS
RETURN (
    SELECT Witcher.[Name] AS NameWitch, Monster.[Name] AS NameMonster, [Order].CountOfMoney
    FROM Witcher JOIN [Order] ON Witcher.Id = [Order].WitcherId
	JOIN Monster ON Monster.Id = [Order].MonsterId
    WHERE [Order].CountOfMoney > 10000
    )
GO


SELECT *
FROM dbo.ExpensiveOrder()
GO

--Многооператорная табличная функция
GO

DROP FUNCTION IF EXISTS dbo.infernalOrder

GO

CREATE FUNCTION dbo.infernalOrder()
RETURNS @orders TABLE 
(
    MonsterName NVARCHAR(50) NOT NULL,
	WitcherName NVARCHAR(50) NOT NULL,
	CountOfMoney INT NOT NULL
)
AS
BEGIN
    INSERT INTO @orders 
    SELECT Monster.[Name] AS MonsterName,Witcher.[Name] AS WitcherName,  [Order].CountOfMoney
    FROM Witcher JOIN [Order] ON Witcher.Id = [Order].WitcherId
	JOIN Monster ON Monster.Id = [Order].MonsterId
    WHERE ThreatLevel = ' infernal' AND CountOfMoney >6000
RETURN
END
GO

SELECT *
FROM dbo.infernalOrder()
-- Рекурсивная функция
GO
    DROP FUNCTION IF EXISTS dbo.GetAllMonsters
GO

CREATE FUNCTION dbo.GetAllMonsters()
RETURNS @AllMonsters TABLE 
(
	Id	INT NOT NULL,  
	NextMonsterId INT,
	[Name] NVARCHAR(50) NOT NULL,
	ThreatLevel NVARCHAR(50) NOT NULL,
	Class NVARCHAR(50) NOT NULL,
	Level INT
)


AS

BEGIN
	DECLARE @NEWMONSTER TABLE (Id INT, NextMonsterId INT, [Name] NVARCHAR(50), ThreatLevel NVARCHAR(50), Class NVARCHAR(50))
	INSERT INTO @NEWMONSTER(Id, NextMonsterId, Name, ThreatLevel, Class)
	SELECT Id, Id + 1, [Name], ThreatLevel, Class
	FROM Monster;

	WITH DirectReports (Id, NextMonsterId, Name, ThreatLevel, Class, Level)
	AS
	(
		SELECT Id, Id + 1 AS NextMonster, Name, ThreatLevel, Class, 0 AS Level
		FROM Monster
		WHERE Id = 1
		UNION ALL


		SELECT N.Id, N.NextMonsterId, N.[Name], D.ThreatLevel, D.Class, Level + 1
		FROM @NEWMONSTER AS N INNER JOIN DirectReports AS D ON N.Id = d.NextMonsterId 
	)

	INSERT INTO @AllMonsters (Id, NextMonsterId, Name, ThreatLevel, Class, Level)
	SELECT Id, NextMonsterId, Name, ThreatLevel, Class, Level
		FROM DirectReports;
	RETURN
END
GO

SELECT *
FROM dbo.GetAllMonsters()



--B. Четыре хранимых процедуры

-- Хранимая процедура без параметров или с параметрами
GO

DROP PROCEDURE IF EXISTS dbo.SelectGolemOrder

GO
CREATE PROCEDURE dbo.SelectGolemOrder AS
BEGIN
    SELECT COUNT(MonsterId) AS countKills FROM dbo.[Order] JOIN dbo.Monster ON dbo.[Order].MonsterId = dbo.Monster.Id
	WHERE Monster.[Name] = 'Golem'
END
GO

EXEC dbo.SelectGolemOrder
GO

--Рекурсивная хранимая процедура или хранимую процедур с рекурсивным ОТВ
GO
DROP PROCEDURE IF EXISTS dbo.SelectMonsterFromId
GO

CREATE PROCEDURE dbo.SelectMonsterFromId
	@monsterid INT
AS
	DECLARE @NEWMONSTER TABLE (Id INT, NextMonsterId INT, [Name] NVARCHAR(50), ThreatLevel NVARCHAR(50), Class NVARCHAR(50))
	INSERT INTO @NEWMONSTER(Id, NextMonsterId, Name, ThreatLevel, Class)
	SELECT Id, Id + 1, [Name], ThreatLevel, Class
	FROM Monster;

	WITH DirectReports (Id, NextMonsterId, Name, ThreatLevel, Class, Level)
	AS
	(
		SELECT Id, Id + 1 AS NextMonster, Name, ThreatLevel, Class, 0 AS Level
		FROM Monster
		WHERE Id = @monsterid
		UNION ALL


		SELECT N.Id, N.NextMonsterId, N.[Name], D.ThreatLevel, D.Class, Level + 1
		FROM @NEWMONSTER AS N INNER JOIN DirectReports AS D ON N.Id = d.NextMonsterId 
	)
	SELECT *
	FROM DirectReports
	WHERE Id <> @monsterid - 1;
GO

EXEC dbo.SelectMonsterFromId @monsterid = 30
GO

--Хранимая процедура с курсором
DROP PROCEDURE IF EXISTS dbo.CursSearch
GO
CREATE PROCEDURE CursSearch AS
BEGIN
-- Объявляем переменную
	DECLARE @TableName nvarchar(255)
	DECLARE @TableCatalog nvarchar(255)
-- Объявляем курсор
	DECLARE TableCursor CURSOR FOR
		SELECT TABLE_NAME, TABLE_CATALOG FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_TYPE = 'BASE TABLE'
-- Открываем курсор и выполняем извлечение первой записи 
		OPEN TableCursor
			FETCH NEXT FROM TableCursor INTO @TableName, @TableCatalog
-- Проходим в цикле все записи из множества
			WHILE @@FETCH_STATUS = 0
			BEGIN
			SELECT @TableName AS NAMETABLE, @TableCatalog AS TABLECATALOG
			FETCH NEXT FROM TableCursor INTO @TableName, @TableCatalog 
			END
		CLOSE TableCursor
	DEALLOCATE TableCursor
END
GO
EXEC CursSearch
GO
-- Хранимую процедуру доступа к метаданным
IF OBJECT_ID ( N'dbo.ScalarFunc', 'P' ) IS NOT NULL
      DROP PROCEDURE dbo.ScalarFunc
GO
CREATE PROCEDURE ScalarFunc 
AS
BEGIN
    SELECT TABLE_NAME
	FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG='WitcherDataBase'
END;
GO

EXECUTE ScalarFunc;
GO

--C. Два DML триггера

--Триггер AFTER
DROP TRIGGER IF EXISTS dbo.Monster_INSERT

GO
CREATE TRIGGER Monster_INSERT
ON Monster
AFTER INSERT
AS
BEGIN
	DECLARE @OrderCount int;
    SET @OrderCount = 5
    WHILE @OrderCount > 0
		BEGIN
			INSERT INTO [Order] (WitcherId, MonsterId,CountOfMoney )
			SELECT ABS(CHECKSUM(NewId()) %100), Id, ABS(CHECKSUM(NewId()) %10000) + 1000
			FROM INSERTED
			SET @OrderCount = @OrderCount - 1;
		END;
END;
GO

INSERT INTO Monster([Name], ThreatLevel, Class, SusceptibilityId)
VALUES('Korun', 'frendly', 'Armenin', 5)

SELECT *
FROM Monster



--Триггер INSTEAD OF
DROP TRIGGER IF EXISTS DenyInsert
GO
CREATE TRIGGER DenyInsert 
ON Susceptibility
INSTEAD OF INSERT
AS
BEGIN
    RAISERROR('You cant add a new susceptibiliti.' ,10, 1);
END;
GO

INSERT INTO Susceptibility(Name, Type)
VALUES('AK47', 'Authomat')

DROP TRIGGER TableIns
DROP TRIGGER Createtable
GO
CREATE TRIGGER Createtable
ON DATABASE
FOR CREATE_TABLE
AS
BEGIN
	declare @x xml = EVENTDATA();
	declare @tableName sysname; 
	set @tableName = @x.value(N'(/EVENT_INSTANCE/ObjectName)[1]', N'sysname');
	SELECT sm.object_id AS TableID, OBJECT_NAME(sm.object_id) AS TableName 
	FROM sys.dm_db_index_physical_stats(DB_ID(),OBJECT_ID(@tableName),null,null,null) as sm 
	SELECT create_date FROM sys.tables where name = @tableName
END
drop table dbo.RAZ
CREATE TABLE DBO.RAZ(ID INT)
