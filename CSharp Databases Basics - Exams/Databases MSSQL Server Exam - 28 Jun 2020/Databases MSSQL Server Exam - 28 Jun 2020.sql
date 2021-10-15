CREATE DATABASE ColonialJourney

USE ColonialJourney

-- 1. DDL

CREATE TABLE Planets (
	[Id] INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(30) NOT NULL,
)

CREATE TABLE Spaceports (
	[Id] INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	[PlanetId] INT FOREIGN KEY REFERENCES [Planets]([Id]) NOT NULL
)

CREATE TABLE Spaceships (
	[Id] INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	[Manufacturer] VARCHAR(30) NOT NULL,
	[LightSpeedRate] INT DEFAULT(0)
)

CREATE TABLE Colonists (
	[Id] INT PRIMARY KEY IDENTITY,
	[FirstName] VARCHAR(20) NOT NULL,
	[LastName] VARCHAR(20) NOT NULL,
	[Ucn] VARCHAR(10) NOT NULL UNIQUE,
	[BirthDate] DATE NOT NULL
)

CREATE TABLE Journeys (
	[Id] INT PRIMARY KEY IDENTITY,
	[JourneyStart] DATETIME NOT NULL,
	[JourneyEnd] DATETIME NOT NULL,
	[Purpose] VARCHAR(11)
	CHECK(Purpose IN ('Medical', 'Technical', 'Educational', 'Military')),
	[DestinationSpaceportId] INT FOREIGN KEY REFERENCES [Spaceports]([Id]) NOT NULL,
	[SpaceshipId]  INT FOREIGN KEY REFERENCES [Spaceships]([Id]) NOT NULL
)

CREATE TABLE TravelCards (
	[Id] INT PRIMARY KEY IDENTITY,
	[CardNumber] CHAR(10) NOT NULL UNIQUE
	CHECK(LEN([CardNumber]) = 10),
	[JobDuringJourney] VARCHAR(8)
	CHECK([JobDuringJourney] IN ('Pilot', 'Engineer', 'Trooper', 'Cleaner', 'Cook')),
	[ColonistId] INT FOREIGN KEY REFERENCES [Colonists]([Id]) NOT NULL,
	[JourneyId] INT FOREIGN KEY REFERENCES [Journeys]([Id]) NOT NULL
)

-- 2. Insert
INSERT INTO Planets ([Name])
VALUES
('Mars'),
('Earth'),
('Jupiter'),
('Saturn')

INSERT INTO Spaceships ([Name],	[Manufacturer],	[LightSpeedRate])
VALUES
('Golf', 'VW', 3),
('WakaWaka', 'Wakanda', 4),
('Falcon9', 'SpaceX', 1),
('Bed',	'Vidolov' ,6)

-- 3. Update

UPDATE Spaceships
SET LightSpeedRate += 1
WHERE Id BETWEEN 8 AND 12

-- 4. Delete

DELETE FROM TravelCards
WHERE JourneyId BETWEEN 1 AND 3

DELETE FROM Journeys
WHERE Id BETWEEN 1 AND 3

-- 5. Select all military journeys

SELECT 
	[Id],
	FORMAT([JourneyStart], 'dd/MM/yyyy') AS [JourneyStart],
	FORMAT([JourneyEnd], 'dd/MM/yyyy') AS [JourneyEnd]
FROM Journeys
WHERE Purpose = 'Military'
ORDER BY JourneyStart

-- 6. Select all pilots

SELECT 
	c.[Id],
	CONCAT(c.[FirstName], ' ', c.[LastName]) 
FROM Colonists AS c
JOIN TravelCards AS tc
ON c.Id = tc.ColonistId
WHERE JobDuringJourney = 'Pilot'
ORDER BY Id

-- 7. Count colonists
SELECT COUNT(*) AS [Count]
FROM Colonists AS c
JOIN TravelCards AS tc
ON c.Id = tc.ColonistId
JOIN Journeys AS j
ON tc.JourneyId = j.Id
WHERE j.Purpose = 'Technical'

-- 8. Select spaceships with pilots younger than 30 years

SELECT ss.[Name], ss.[Manufacturer] FROM Spaceships AS ss
JOIN Journeys AS j
ON ss.Id = j.SpaceshipId
JOIN TravelCards AS tc
ON j.Id = tc.JourneyId
JOIN Colonists AS c
ON tc.ColonistId = c.Id
WHERE tc.JobDuringJourney = 'Pilot' AND DATEDIFF(YEAR, BirthDate, '01/01/2019') < 30
ORDER BY ss.[Name]

-- 9. Select all planets and their journey count

SELECT 
	p.[Name] AS PlanetName,
	COUNT(j.Id) AS [JourneysCount] FROM Planets AS p
JOIN Spaceports AS sp
ON p.Id = sp.PlanetId
JOIN Journeys AS j
ON sp.Id = j.DestinationSpaceportId
GROUP BY p.[Name]
ORDER BY [JourneysCount] DESC, p.[Name]

-- 10. Select Second Oldest Important Colonist
SELECT * FROM (
SELECT tc.JobDuringJourney,
	CONCAT(c.[FirstName], ' ', c.[LastName]) AS [FUll Name], 
	DENSE_RANK ( ) OVER (PARTITION BY tc.JobDuringJourney ORDER BY c.BirthDate) AS [Rank] FROM Colonists AS c
JOIN TravelCards AS tc
ON c.Id = tc.ColonistId
--WHERE c.FirstName = 'Hale'
) AS Queery
WHERE [Rank] = 2

-- 11. Get Colonists Count

GO

CREATE FUNCTION dbo.udf_GetColonistsCount(@PlanetName VARCHAR (30)) 
RETURNS INT
AS
BEGIN
	DECLARE @Count INT
	SET @Count =
	(SELECT COUNT(*) AS [Count] FROM Colonists AS c
	JOIN TravelCards AS tc
	ON c.Id = tc.ColonistId
	JOIN Journeys AS j
	ON tc.JourneyId = j.Id
	JOIN Spaceports AS s
	ON j.DestinationSpaceportId = s.Id
	JOIN Planets AS p
	ON s.PlanetId = p.Id
	WHERE p.[Name] = @PlanetName)

	RETURN @Count
END

SELECT dbo.udf_GetColonistsCount('Otroyphus')

-- 12. Change Journey Purpose

CREATE PROCEDURE usp_ChangeJourneyPurpose(@JourneyId INT , @NewPurpose VARCHAR(11))
AS
BEGIN
	DECLARE @bool BIT
	SET @bool = 0

	IF NOT EXISTS (SELECT 1 FROM Journeys WHERE Id = @JourneyId)
	BEGIN
		SELECT 'The journey does not exist!'
		SET @bool = 1
	END

	IF ((SELECT Purpose FROM Journeys WHERE Id = @JourneyId) = @NewPurpose)
	BEGIN
		SELECT 'You cannot change the purpose!'
		SET @bool = 1
	END

	IF(@bool = 0) 
	BEGIN
	UPDATE Journeys
	SET Purpose = @NewPurpose
	WHERE Id = @JourneyId
	END
END

EXEC usp_ChangeJourneyPurpose 2, 'Educational'
EXEC usp_ChangeJourneyPurpose 196, 'Technical'
EXEC usp_ChangeJourneyPurpose 4, 'Technical'
SELECT * FROM Journeys WHERE Id = 4