CREATE DATABASE TripService

USE TripService

-- 1. DDL

CREATE TABLE Cities (
	[Id] INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(20) NOT NULL,
	[CountryCode] CHAR(2) NOT NULL
)

CREATE TABLE Hotels (
	[Id] INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL,
	[CityId] INT FOREIGN KEY REFERENCES [Cities]([Id]) NOT NULL,
	[EmployeeCount] INT NOT NULL,
	[BaseRate] DECIMAL (18,2)
)

CREATE TABLE Rooms (
	[Id] INT PRIMARY KEY IDENTITY,
	[Price] DECIMAL(18,2) NOT NULL,
	[Type] NVARCHAR(20) NOT NULL,
	[Beds] INT NOT NULL,
	[HotelId] INT FOREIGN KEY REFERENCES [Hotels]([Id]) NOT NULL
)

CREATE TABLE Trips (
	[Id] INT PRIMARY KEY IDENTITY,
	[RoomId] INT FOREIGN KEY REFERENCES [Rooms]([Id]) NOT NULL,
	[BookDate] DATE NOT NULL,
	[ArrivalDate] DATE NOT NULL,
	[ReturnDate] DATE NOT NULL,
	[CancelDate] DATE,
	CHECK([BookDate] < [ArrivalDate]),
	CHECK([ArrivalDate] < [ReturnDate])
)

CREATE TABLE Accounts (
	[Id] INT PRIMARY KEY IDENTITY,
	[FirstName] NVARCHAR(50) NOT NULL,
	[MiddleName] NVARCHAR(20),
	[LastName] NVARCHAR(50) NOT NULL,
	[CityId] INT FOREIGN KEY REFERENCES [Cities]([Id]) NOT NULL,
	[BirthDate] DATE NOT NULL,
	[Email] VARCHAR(100) NOT NULL UNIQUE
)

CREATE TABLE AccountsTrips (
	[AccountId] INT FOREIGN KEY REFERENCES [Accounts]([Id]) NOT NULL,
	[TripId] INT FOREIGN KEY REFERENCES [Trips]([Id]) NOT NULL,
	[Luggage] INT NOT NULL
	CHECK([Luggage] >= 0)
	PRIMARY KEY([AccountId], [TripId])
)

-- 2.Insert
INSERT INTO Accounts ([FirstName], [MiddleName], [LastName], [CityId], [BirthDate], [Email])
VALUES
('John',	'Smith', 'Smith', 	34, 	'1975-07-21'	,'j_smith@gmail.com'),
('Gosho',	NULL, 'Petrov', 11 ,'1978-05-16', 'g_petrov@gmail.com'),
('Ivan',	'Petrovich', 'Pavlov', 59, '1849-09-26', 'i_pavlov@softuni.bg'),
('Friedrich',	'Wilhelm', 'Nietzsche', 2, '1844-10-15', 'f_nietzsche@softuni.bg')

INSERT INTO Trips ([RoomId], [BookDate], [ArrivalDate], [ReturnDate], [CancelDate])
VALUES
(101, '2015-04-12', '2015-04-14', '2015-04-20', '2015-02-02'),
(102, '2015-07-07', '2015-07-15', '2015-07-22', '2015-04-29'),
(103, '2013-07-17', '2013-07-23', '2013-07-24', NULL),
(104, '2012-03-17', '2012-03-31', '2012-04-01', '2012-01-10'),
(109, '2017-08-07', '2017-08-28', '2017-08-29', NULL)

-- 3.Update

UPDATE Rooms
SET Price *= 1.14
WHERE [HotelId] IN (5,7,9)

-- 4.Delete

DELETE FROM AccountsTrips
WHERE AccountId = 47

-- 5. EEE-Mails

SELECT 
	a.FirstName,
	a.LastName,
	FORMAT(BirthDate, 'MM-dd-yyyy'),
	c.[Name],
	a.Email
FROM Accounts AS a
JOIN Cities AS c
ON a.CityId = c.Id
WHERE Email LIKE 'e%'
ORDER BY c.[Name]

-- 6. City Statistics

SELECT c.[Name], COUNT(h.[Id]) FROM Cities AS c
JOIN Hotels AS h
ON h.CityId = c.Id
GROUP BY c.[Name]
ORDER BY COUNT(h.[Id]) DESC, c.[Name]

-- 7. Longest and Shortest Trips

SELECT 
a.Id, 
CONCAT(FirstName, ' ' , LastName) AS [Full Name],
MAX(DATEDIFF(DAY, ArrivalDate, ReturnDate)) AS [Max],
MIN(DATEDIFF(DAY, ArrivalDate, ReturnDate)) AS [Min]
FROM Accounts AS a
JOIN AccountsTrips AS ac
ON a.Id = ac.AccountId
JOIN Trips AS t
ON ac.TripId = t.Id
WHERE MiddleName IS NULL AND t.CancelDate IS NULL
GROUP BY a.Id, CONCAT(FirstName, ' ' , LastName)
ORDER BY [MAX] DESC, [MIN]

-- 8. Metropolis

SELECT TOP (10) c.[Id], c.[Name], c.[CountryCode], COUNT(a.[Id]) AS Accounts FROM Cities AS c
JOIN Accounts AS a
ON c.Id = a.CityId
GROUP BY c.[Id], c.[Name], c.[CountryCode]
ORDER BY COUNT(a.[Id]) DESC

-- 9. Romantic Getaways

SELECT a.Id,a.Email, c.[Name] AS City, COUNT(t.Id) AS Trips FROM Accounts AS a
JOIN Cities AS c
ON a.CityId = c.Id
JOIN AccountsTrips AS [at]
ON [at].AccountId = a.Id
JOIN Trips AS t
ON [at].TripId = t.Id
JOIN Rooms AS r
ON t.RoomId = r.Id
JOIN Hotels AS h
ON r.HotelId = h.Id
WHERE h.CityId = c.Id
GROUP BY a.Id,a.Email, c.[Name]
ORDER BY Trips DESC, a.Id

-- 10. GDPR Violation

SELECT
t.Id,
CONCAT(a.FirstName, ' ' ,
CASE WHEN a.MiddleName IS NULL THEN '' 
ELSE CONCAT(a.MiddleName, ' ') END , a.LastName) AS [Full Name],
c.[Name],
(SELECT TOP (1) [Name] FROM Cities WHERE h.CityId = Id) AS HoteCity,
CASE 
WHEN t.CancelDate IS NULL THEN CONCAT(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate), ' days')
ELSE  'Canceled' END AS Duration
FROM Accounts AS a
JOIN Cities AS c
ON a.CityId = c.Id
JOIN AccountsTrips AS [at]
ON [at].AccountId = a.Id
JOIN Trips AS t
ON [at].TripId = t.Id
JOIN Rooms AS r
ON t.RoomId = r.Id
JOIN Hotels AS h
ON r.HotelId = h.Id
WHERE t.Id IS NOT NULL
ORDER BY [Full Name], t.Id

-- 11.

-- 12. Switch Room 4/7 O_O 
GO

CREATE PROCEDURE usp_SwitchRoom(@TripId INT , @TargetRoomId INT)
AS
BEGIN
IF(
	(SELECT TOP (1) r.HotelId FROM Trips AS t 
	JOIN Rooms AS r
	ON t.RoomId = r.Id
	WHERE t.Id = @TripId)
	!= 
	(SELECT TOP (1) r.HotelId FROM Trips AS t 
	JOIN Rooms AS r
	ON t.RoomId = r.Id
	WHERE r.Id = @TargetRoomId))
	BEGIN
		SELECT 'Target room is in another hotel!'
	END

ELSE IF(
	(SELECT TOP (1) r.Beds FROM Trips AS t 
	JOIN Rooms AS r
	ON t.RoomId = r.Id
	WHERE t.Id = @TripId)
	>
	(SELECT TOP (1) r.Beds FROM Rooms AS r
	WHERE r.Id = @TargetRoomId))
	BEGIN
		SELECT 'Not enough beds in target room!'
	END
ELSE 
	BEGIN
		UPDATE Trips
		SET RoomId = @TargetRoomId
		WHERE Id = @TripId
	END

END

EXEC usp_SwitchRoom 10, 11
SELECT RoomId FROM Trips WHERE Id = 10
EXEC usp_SwitchRoom 10, 8
