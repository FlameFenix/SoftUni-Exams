CREATE DATABASE [Service]

USE [Service]

CREATE TABLE [Users] (
[Id] INT PRIMARY KEY IDENTITY,
[Username] NVARCHAR(30) UNIQUE NOT NULL,
[Password] NVARCHAR(50) NOT NULL,
[Name] NVARCHAR(50),
[Birthdate] DATETIME2,
[Age] INT CHECK (Age BETWEEN 14 AND 110),
[Email] NVARCHAR(50) NOT NULL
)

CREATE TABLE [Departments] (
[Id] INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(50) NOT NULL,
)

CREATE TABLE [Employees](
[Id] INT PRIMARY KEY IDENTITY,
[FirstName] NVARCHAR(25),
[LastName] NVARCHAR(25),
[Birthdate] DATETIME2,
[Age] INT CHECK (Age BETWEEN 18 AND 110),
[DepartmentId] INT FOREIGN KEY REFERENCES [Departments](Id)
)

CREATE TABLE [Categories] (
[Id] INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(50) NOT NULL,
[DepartmentId] INT FOREIGN KEY REFERENCES [Departments](Id) NOT NULL,
)

CREATE TABLE [Status] (
[Id] INT PRIMARY KEY IDENTITY,
[Label] NVARCHAR(30) NOT NULL
)

CREATE TABLE [Reports] (
[Id] INT PRIMARY KEY IDENTITY,
[CategoryId] INT FOREIGN KEY REFERENCES [Categories]([Id]),
[StatusId] INT FOREIGN KEY REFERENCES [Status]([Id]) NOT NULL,
[OpenDate] DATETIME2 NOT NULL,
[CloseDate] DATETIME2,
[Description] NVARCHAR(200),
[UserId] INT FOREIGN KEY REFERENCES [Users]([Id]) NOT NULL,
[EmployeeId] INT FOREIGN KEY REFERENCES [Employees]([Id])
)
INSERT INTO Employees([FirstName], [LastName], [Birthdate], [DepartmentId])
VALUES
('Marlo', 'O''Malley', '1958-9-21',	1),
('Niki', 'Stanaghan', '1969-11-26',	4),
('Ayrton','Senna', '1960-03-21', 9),
('Ronnie',	'Peterson',	'1944-02-14', 9),
('Giovanna', 'Amati', '1959-07-20', 5)

INSERT INTO Reports([CategoryId],[StatusId], [OpenDate],	[CloseDate]	,[Description]	,[UserId]	,[EmployeeId])
VALUES
(1,	1,	'2017-04-13', NULL,	'Stuck Road on Str.133', 6,	2),
(6,	3,	'2015-09-05', '2015-12-06' ,'Charity trail running', 3,	5),
(14, 2,	'2015-09-07', NULL,	'Falling bricks on Str.58',	5,	2),
(4,	3,	'2017-07-03', '2017-07-06' ,'Cut off streetlight on Str.11', 1,	1)

UPDATE Reports
SET CloseDate = GETDATE()

DELETE Reports
WHERE StatusId = 4

SELECT [Description], FORMAT([OpenDate], 'dd-MM-yyyy') AS [OpenDate] FROM Reports AS r
WHERE r.[EmployeeId] IS NULL
ORDER BY r.[OpenDate], r.[Description] ASC

SELECT r.[Description], c.[Name] FROM Reports AS r
LEFT JOIN Categories AS c
ON r.[CategoryId] = c.[Id]
ORDER BY r.[Description], c.[Name]

SELECT TOP (5) c.[Name], COUNT(r.[Id]) FROM Reports AS r
LEFT JOIN Categories AS c
ON r.CategoryId = c.Id
GROUP BY c.[Name]
ORDER BY COUNT(r.[Id]) DESC , c.[Name] ASC

SELECT u.[Username], c.[Name] FROM Reports AS r
JOIN Users AS u
ON r.UserId = u.Id
JOIN Categories AS c
ON r.[CategoryId] = c.Id
WHERE FORMAT(r.OpenDate, 'dd-MM') = FORMAT(u.Birthdate, 'dd-MM')
ORDER BY u.[Username], c.[Name]

SELECT [FullName], COUNT(Username) FROM 
(
SELECT CONCAT(e.FirstName, ' ', e.LastName) AS FullName,
r.UserId AS Username
FROM Employees AS e
LEFT JOIN Reports AS r 
ON r.EmployeeId = e.Id
) AS SubQuerry
GROUP BY [FullName]
ORDER BY COUNT(Username) DESC, FullName

SELECT 
CASE WHEN CONCAT(FirstName, ' ', LastName) = ' ' THEN 'None' ELSE CONCAT(FirstName, ' ', LastName) END AS Employee,
ISNULL(d.[Name], 'None') AS Department,
c.[Name] AS Category,
r.[Description] AS [Description],
FORMAT(r.[OpenDate], 'dd.MM.yyyy') AS [OpenDate],
s.[Label] AS [Status],
u.[Name] AS [User]
FROM Reports AS r
LEFT JOIN Employees AS e
ON r.EmployeeId = e.Id
LEFT JOIN Departments AS d
ON e.DepartmentId = d.Id
LEFT JOIN [Status] AS s
ON s.Id = r.StatusId
LEFT JOIN Users AS u
ON r.UserId = u.Id
LEFT JOIN Categories AS c
ON r.CategoryId = c.Id
ORDER BY [FirstName] DESC, [LastName] DESC, [Department], [Category], [Description], OpenDate, [Status], [User] 

CREATE FUNCTION udf_HoursToComplete(@StartDate DATETIME, @EndDate DATETIME)
RETURNS INT
AS
BEGIN
IF(@StartDate IS NULL OR @EndDate IS NULL)
	BEGIN
		RETURN 0
	END
RETURN DATEDIFF(HOUR, @StartDate, @EndDate)
END

SELECT dbo.udf_HoursToComplete(OpenDate, CloseDate) AS TotalHours
   FROM Reports
