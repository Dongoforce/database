sp_configure 'show advanced options', 1
GO
RECONFIGURE
GO
sp_configure 'clr enabled', 1
GO
RECONFIGURE
GO

DROP FUNCTION GetFact;
DROP AGGREGATE CountMoney;
DROP FUNCTION StringLength;
DROP PROCEDURE AverageNumberOfKills;
DROP TRIGGER DeleteSusceptibility;
DROP TYPE Threat;
DROP TABLE dbo.TestMonster;
DROP ASSEMBLY SqlFunc;

CREATE ASSEMBLY SqlFunc
AUTHORIZATION dbo
FROM 'E:\Witcher\witchfunc\witchfunc\bin\Debug\witchfunc.dll'
WITH PERMISSION_SET = SAFE
GO

-- 1) Определяемую пользователем скалярную функцию CLR 
CREATE FUNCTION GetFact (@num int)
RETURNS INT
AS
EXTERNAL NAME
SqlFunc.[SqlFunc].GetFact
GO
SELECT dbo.GetFact(2) AS Factorial
GO
-- 2) Пользовательскую агрегатную функцию CLR 
CREATE AGGREGATE CountMoney( @instr int )
RETURNS INT
EXTERNAL NAME
SqlFunc.[SqlAggregate1]
GO

SELECT dbo.CountMoney(CountOfMoney) AS MoneyMoreThan1000
FROM WitcherDataBase.dbo.[Order]
GO
SELECT* FROM WitcherDataBase.dbo.[Order]

-- 3) Определяемую пользователем табличную функцию CLR 
go
CREATE FUNCTION StringLength ( @input NVARCHAR(4000) )
RETURNS TABLE 
(
   word NVARCHAR(4000), 
   len INT
)
AS
EXTERNAL NAME
SqlFunc.[UserDefinedFunctions].TableFunction
GO

SELECT * FROM dbo.StringLength('Witcher')
GO
-- 4) Хранимую процедуру CLR

CREATE PROCEDURE AverageNumberOfKills( @bindingg NVARCHAR(4000) )
AS
External Name
SqlFunc.[StoredProcedures].AvgStuffNum
GO

EXEC AverageNumberOfKills 'Jack'
GO

--5) Триггер
CREATE TRIGGER DeleteSusceptibility
ON Susceptibility
INSTEAD OF DELETE
AS
EXTERNAL NAME
SqlFunc.[Triggers].SqlTrigger1
GO

DELETE Susceptibility
WHERE Id = 1
GO

--6) Определяемы пользователем тип данных

CREATE TYPE dbo.Threat
EXTERNAL NAME SqlFunc.[Threat];
GO

CREATE TABLE dbo.TestMonster
( 
  id INT IDENTITY(1,1) NOT NULL, 
  ThreatLevel Threat NOT NULL,
  [Name] NVARCHAR(50) NOT NULL
);
GO


INSERT INTO dbo.TestMonster(ThreatLevel, [Name]) VALUES('low', 'Koryun'); 
SELECT id, ThreatLevel.ToString() AS ThreatLevel, [Name]
FROM dbo.TestMonster;

