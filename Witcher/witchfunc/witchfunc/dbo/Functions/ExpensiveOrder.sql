CREATE FUNCTION dbo.ExpensiveOrder()
RETURNS TABLE
AS
RETURN (
    SELECT Witcher.[Name] AS NameWitch, Monster.[Name] AS NameMonster, [Order].CountOfMoney
    FROM Witcher JOIN [Order] ON Witcher.Id = [Order].WitcherId
	JOIN Monster ON Monster.Id = [Order].MonsterId
    WHERE [Order].CountOfMoney > 10000
    )
