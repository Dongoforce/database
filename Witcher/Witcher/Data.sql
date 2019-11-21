BULK INSERT WitcherDataBase.dbo.Monster
FROM 'E:\Witcher\Witcher\Monster.csv'
WITH (DATAFILETYPE = 'char', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');
GO 

BULK INSERT WitcherDataBase.dbo.Susceptibility
FROM 'E:\Witcher\Witcher\Susceptibility.csv'
WITH (DATAFILETYPE = 'char', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');
GO 

BULK INSERT WitcherDataBase.dbo.Witcher
FROM 'E:\Witcher\Witcher\Witcher.csv'
WITH (DATAFILETYPE = 'char', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');
GO 

BULK INSERT WitcherDataBase.dbo.[Order]
FROM 'E:\Witcher\Witcher\Order.csv'
WITH (DATAFILETYPE = 'char', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');
GO 

