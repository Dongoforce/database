
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
