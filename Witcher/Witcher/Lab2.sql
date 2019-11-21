/*_________________________(1)____________________________*/
SELECT Id, Name 
FROM WitcherDataBase.dbo.Witcher
WHERE SkillLevel = 'professional'

/*_________________________(2)____________________________*/
SELECT WitcherId, MonsterId, CountOfMoney
FROM WitcherDataBase.dbo.[Order] JOIN WitcherDataBase.dbo.Witcher ON WitcherDataBase.dbo.Witcher.Id =   WitcherDataBase.dbo.[Order].WitcherId
Where CountOfMoney BETWEEN (2000) AND (4000)

/*_________________________(3)____________________________*/
SELECT Name, ThreatLevel, Class
FROM WitcherDataBase.dbo.Monster
WHERE ThreatLevel LIKE '%l%' 

/*_________________________(4)____________________________*/
SELECT WitcherId, MonsterId, CountOfMoney
FROM WitcherDataBase.dbo.[Order]
WHERE WitcherId IN
	(
		SELECT Id
		FROM WitcherDataBase.dbo.Witcher
		WHERE SkillLevel = 'beginner'
	) AND CountOfMoney > 200

/*_________________________(5)____________________________*/
SELECT DISTINCT Id, Name, ThreatLevel
From WitcherDataBase.dbo.Monster
WHERE NOT EXISTS 
	(
		SELECT MonsterId
		FROM WitcherDataBase.dbo.[Order]
		WHERE Monster.Id =  [Order].MonsterId
	)

/*_________________________(6)____________________________*/
SELECT *
FROM WitcherDataBase.dbo.Witcher
WHERE NumberOfKills > ALL
	(
		SELECT NumberOfKills
		FROM WitcherDataBase.dbo.Witcher
		WHERE SkillLevel = 'Master'	
	)

/*_________________________(7)____________________________*/
SELECT AVG(TotCount) AS 'Actual AVG',
	SUM(TotCount) / COUNT(Id) AS 'Calc AVG'

FROM
	(
		SELECT Id, SUM(CountOfMoney) AS TotCount
		FROM WitcherDataBase.dbo.[Order]
		GROUP BY Id
	) AS BigMoney

/*_________________________(8)____________________________*/
SELECT id, Name,
	(
		SELECT AVG(CountOfMoney)
		FROM WitcherDataBase.dbo.[Order]
		WHERE [Order].WitcherId = WitcherDataBase.dbo.Witcher.Id
	) AS AvgMoney,
	(
		SELECT MIN(CountOfMoney)
		FROM WitcherDataBase.dbo.[Order]
		WHERE [Order].WitcherId = WitcherDataBase.dbo.Witcher.Id
	) AS MinMoney
	FROM WitcherDataBase.dbo.Witcher


SELECT * 
FROM WitcherDataBase.dbo.[Order]
WHERE WitcherId IN
	(
		SELECT Id
		FROM WitcherDataBase.dbo.Witcher
		WHERE Name = 'Blake'
	)

/*_________________________(9)____________________________*/
SELECT  WitcherId, Name, CountOfMoney, ThreatLevel,
	CASE ThreatLevel
		WHEN ' loW' THEN 'It becomes easy'
		WHEN ' medium' THEN 'Oh my, its normal'
		WHEN ' high' THEN 'It becomes not so easy'
		ELSE 'RUN STUPID BOY'
	END AS 'How it will be?'
FROM WitcherDataBase.dbo.[Order] JOIN WitcherDataBase.dbo.Monster ON [Order].MonsterId = Monster.Id

/*_________________________(10)____________________________*/
SELECT  MonsterId, Name, CountOfMoney,
	CASE
		WHEN CountOfMoney > 7000 THEN 'Over Price'
		WHEN CountOfMoney < 100 THEN 'Bad Price'
		ELSE 'Normal Price'
	END AS 'How much'
FROM WitcherDataBase.dbo.[Order] JOIN WitcherDataBase.dbo.WITCHER ON [Order].WitcherId = Witcher.Id

/*_________________________(11)____________________________*/
SELECT WitcherId,
 SUM(CountOfMoney) AS SM,
 CAST(AVG(CountOfMoney)AS money) AS SR
INTO #BestPrice
FROM WitcherDataBase.dbo.[Order]
WHERE WitcherId IS NOT NULL
GROUP BY WitcherId
SELECT * FROM #BestPrice

/*_________________________(12)____________________________*/
SELECT 'By Witchers' AS Criteria,Name as 'Richest', CM AS COUNOFMONEY
FROM WitcherDataBase.dbo.Witcher W JOIN
	(
		SELECT TOP 1 WitcherId, AVG(CountOfMoney) AS CM
		FROM WitcherDataBase.dbo.[Order] 
		GROUP BY WitcherId
		ORDER BY CM DESC
	) AS O ON O.WitcherId = W.Id

/*_________________________(13)____________________________*/
SELECT 'RichestSM' as Criteria, SUM(CountOfMoney) AS CountOfMoney
FROM WitcherDataBase.dbo.[Order]
WHERE WitcherId IN
(
	SELECT WitcherId
	FROM WitcherDataBase.dbo.[Order]
	GROUP BY WitcherId 
	HAVING SUM(CountOfMoney) = 
		(
			SELECT MAX(CM)
			FROM
			(
				SELECT SUM(CountOfMoney) AS CM
				FROM WitcherDataBase.dbo.[Order]
				GROUP BY WitcherId
		    ) AS MD
		)
)

/*_________________________(14)____________________________*/
SELECT
	W.Id,
	W.Name,
	OD.CountOfMoney,
	AVG(CountOfMoney) AS AvgMoney,
	MIN(CountOfMoney) AS MinMoney
FROM WitcherDataBase.dbo.Witcher W LEFT OUTER JOIN WitcherDataBase.dbo.[Order] OD ON OD.WitcherId = W.Id
WHERE OD.CountOfMoney IS NOT NULL
GROUP BY W.Id, W.Name, OD.CountOfMoney

/*_________________________(15)____________________________*/
SELECT WitcherId, AVG(CountOfMoney) AS 'Average money'
FROM WitcherDataBase.dbo.[Order] O
GROUP BY WitcherId
HAVING AVG(CountOfMoney) >
	(
		SELECT AVG(CountOfMoney) AS MO
		FROM WitcherDataBase.dbo.[Order]
	)

/*_________________________(16)____________________________*/
INSERT WitcherDataBase.dbo.Monster(Name, ThreatLevel, Class, SusceptibilityId)
VALUES ('Korun', 'Infamous', 'Person', 22);

/*_________________________(17)____________________________*/
INSERT WitcherDataBase.dbo.Witcher(Name, SkillLevel, NumberOfKills)
SELECT
	(
		SELECT Name
		FROM WitcherDataBase.dbo.Witcher
		WHERE Id = 5
	), SkillLevel, NumberOfKills 
	FROM WitcherDataBase.dbo.Witcher
	WHERE NumberOfKills = 175

SELECT *
FROM WitcherDataBase.dbo.Witcher

/*_________________________(18)____________________________*/
UPDATE WitcherDataBase.dbo.[Order]
SET CountOfMoney = CountOfMoney * 1.5
WHERE Id = 35

SELECT *
FROM WitcherDataBase.dbo.[Order]
WHERE Id = 35

/*_________________________(19)____________________________*/
UPDATE WitcherDataBase.dbo.[Order]
SET CountOfMoney =
	(
		SELECT AVG(CountOfMoney)
		FROM WitcherDataBase.dbo.[Order]
		WHERE WitcherId = 35
	)
WHERE WitcherId = 35

SELECT *
FROM WitcherDataBase.dbo.[Order]
WHERE WitcherId = 35


/*_________________________(20)____________________________*/
DELETE WitcherDataBase.dbo.[Order]
WHERE CountOfMoney > 9900

SELECT *
FROM WitcherDataBase.dbo.[Order]
WHERE CountOfMoney > 9900

/*_________________________(21)____________________________*/
DELETE FROM WitcherDataBase.dbo.[Order]
WHERE WitcherId IN
    (
        SELECT Id
        FROM WitcherDataBase.dbo.Witcher
        WHERE NumberOfKills = 0
    )

/*_________________________(22)____________________________*/
WITH CTE (WitcherNo, CountOfMoney)
AS
	(
		SELECT Id, CountOfMoney
		FROM WitcherDataBase.dbo.[Order]
		WHERE Id IS NOT NULL
		GROUP BY Id, CountOfMoney
	)
SELECT AVG(CountOfMoney) AS 'Среднее количество денег'
FROM CTE

/*_________________________(23)____________________________*/
SELECT Id, Id + 1 AS NextMonsterId, Name, ThreatLevel, Class
INTO #NewMonster
FROM WitcherDataBase.dbo.Monster
SELECT * FROM dbo.#NewMonster;

WITH DirectReports (Id, NextMonsterId, Name, ThreatLevel, Class, Level)
AS
(
		SELECT Id, NextMonsterId, Name, ThreatLevel, Class, 0 AS Level
		FROM #NewMonster AS NM
		WHERE Id = 1
		UNION ALL


		SELECT NM.Id, NM.NextMonsterId, NM.Name, NM.ThreatLevel, NM.Class, Level + 1
		FROM dbo.#NewMonster AS NM INNER JOIN DirectReports AS D ON NM.ID = D.NextMonsterId
)
SELECT Id, NextMonsterId, Name, ThreatLevel, Class, Level
FROM DirectReports

/*_________________________(24)____________________________*/
SELECT
	WitcherId,
	AVG(CountOfMoney) OVER(PARTITION BY WitcherId) AS AvgMoney
FROM WitcherDataBase.dbo.[Order]

/*_________________________(25)____________________________*/
go
WITH DELDOUBLE
AS
(
	SELECT WitcherId, MonsterId, CountOfMoney, row_number() OVER (PARTITION BY WitcherId ORDER BY WitcherId) rn
	FROM WitcherDataBase.dbo.[Order]
)
DELETE
FROM DELDOUBLE
WHERE rn > 1

SELECT * FROM WitcherDataBase.dbo.[Order]