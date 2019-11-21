
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
