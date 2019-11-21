/*EXEC sp_configure 'xp_cmdshell', 1;  
GO  
RECONFIGURE;  
GO  
DECLARE @result int
DECLARE @OutputFileName varchar(150)
DECLARE @cmd varchar( 500)

Set @OutputFileName = 'c:\mysql.xml'

Set @cmd = 'BCP "SELECT DISTINCT Witcher.[Name] AS WitcherName, Monster.[Name] AS MonsterName, [Order].CountOfMoney
FROM [Order] JOIN Monster ON [Order].MonsterId = Monster.Id JOIN Witcher ON [Order].WitcherId = Witcher.Id
FOR XML AUTO, ROOT("Orders") "' + @OutputFileName + '" -C ACP -c -r -T'

EXEC @result = master..xp_cmdshell @cmd*/

--1_1
SELECT DISTINCT Witcher.[Name] AS WitcherName, Monster.[Name] AS MonsterName, [Order].CountOfMoney
FROM [Order] JOIN Monster ON [Order].MonsterId = Monster.Id JOIN Witcher ON [Order].WitcherId = Witcher.Id
FOR XML AUTO, ROOT('Orders')
--1_2
SELECT DISTINCT Witcher.[Name] AS WitcherName, Monster.[Name] AS MonsterName, [Order].CountOfMoney
FROM [Order] JOIN Monster ON [Order].MonsterId = Monster.Id JOIN Witcher ON [Order].WitcherId = Witcher.Id
FOR XML RAW, TYPE
--1_3
SELECT DISTINCT Witcher.[Name] AS WitcherName, Monster.[Name] AS MonsterName, [Order].CountOfMoney
FROM [Order] JOIN Monster ON [Order].MonsterId = Monster.Id JOIN Witcher ON [Order].WitcherId = Witcher.Id
FOR XML RAW('Order'), TYPE, ELEMENTS, ROOT('Orders')
--1_4
SELECT 1 AS Tag,
		NULL AS PARENT,
		[Order].Id AS[Order!1!Id],
		Witcher.[Name] AS [Order!1!WitcherName!ELEMENT], 
		Monster.[Name] AS [Order!1!MonsterName!ELEMENT], 
		[Order].CountOfMoney AS [Order!1!CountOfMoney!ELEMENT]
FROM [Order] JOIN Monster ON [Order].MonsterId = Monster.Id JOIN Witcher ON [Order].WitcherId = Witcher.Id
FOR XML EXPLICIT, TYPE, ROOT('Orders')

--2
DECLARE @iord int
DECLARE @ord xml
SELECT @ord = c FROM OPENROWSET(BULK 'E:\Witcher\task1_3.xml', SINGLE_BLOB) AS TEMP(c)
EXEC sp_xml_preparedocument @iord OUTPUT, @ord
SELECT *
FROM OPENXML (@iord, N'/Orders/Order',2)
WITH (WitcherName NVARCHAR(100) , MonsterName NVARCHAR(6), CountOfMoney INT)
EXEC sp_xml_removedocument @iord

SET IDENTITY_INSERT Witcher ON
reconfigure