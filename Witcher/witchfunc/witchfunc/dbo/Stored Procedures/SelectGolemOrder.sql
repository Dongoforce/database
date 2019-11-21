CREATE PROCEDURE dbo.SelectGolemOrder AS
BEGIN
    SELECT COUNT(MonsterId) AS countKills FROM dbo.[Order] JOIN dbo.Monster ON dbo.[Order].MonsterId = dbo.Monster.Id
	WHERE Monster.[Name] = 'Golem'
END
