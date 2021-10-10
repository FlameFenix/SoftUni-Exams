CREATE DATABASE [WMS]

USE [WMS]

CREATE TABLE [Clients] (
[ClientId] INT PRIMARY KEY IDENTITY NOT NULL,
[FirstName] VARCHAR(50) NOT NULL,
[LastName] VARCHAR(50) NOT NULL,
[Phone] CHAR(12) NOT NULL,
CHECK(LEN([Phone]) = 12) 
)

CREATE TABLE [Mechanics] (
[MechanicId] INT PRIMARY KEY IDENTITY NOT NULL,
[FirstName] VARCHAR(50) NOT NULL,
[LastName] VARCHAR(50) NOT NULL,
[Address] VARCHAR(255) NOT NULL
)

CREATE TABLE [Models] (
[ModelId] INT PRIMARY KEY IDENTITY NOT NULL,
[Name] VARCHAR(50) NOT NULL UNIQUE
)

CREATE TABLE [Jobs] ( 
[JobId] INT PRIMARY KEY IDENTITY NOT NULL,
[ModelId] INT FOREIGN KEY REFERENCES [Models]([ModelId]) NOT NULL,
[Status] VARCHAR(11) DEFAULT('Pending') NOT NULL ,
CHECK([Status] IN ('Pending', 'In Progress', 'Finished')),
[ClientId] INT FOREIGN KEY REFERENCES [Clients]([ClientId]) NOT NULL,
[MechanicId] INT FOREIGN KEY REFERENCES [Mechanics]([MechanicId]),
[IssueDate] DATE NOT NULL,
[FinishDate] DATE
)

CREATE TABLE [Orders] (
[OrderId] INT PRIMARY KEY IDENTITY NOT NULL,
[JobId] INT FOREIGN KEY REFERENCES [Jobs]([JobId]) NOT NULL,
[IssueDate] DATE,
[Delivered] BIT DEFAULT(0) NOT NULL,
)

CREATE TABLE [Vendors] ( 
[VendorId] INT PRIMARY KEY IDENTITY NOT NULL,
[Name] VARCHAR(50) NOT NULL UNIQUE
)

CREATE TABLE [Parts] ( 
[PartId] INT PRIMARY KEY IDENTITY NOT NULL,
[SerialNumber] VARCHAR(50) NOT NULL UNIQUE,
[Description] VARCHAR(255),
[Price] DECIMAL(6, 2) NOT NULL,
CHECK([Price] > 0),
[VendorId] INT FOREIGN KEY REFERENCES [Vendors]([VendorId]) NOT NULL,
[StockQty] INT DEFAULT(0) NOT NULL,
CHECK([StockQty] >= 0)
)

CREATE TABLE [OrderParts] (
[OrderId] INT FOREIGN KEY REFERENCES [Orders]([OrderId]) NOT NULL,
[PartId] INT FOREIGN KEY REFERENCES [Parts]([PartId]) NOT NULL,
[Quantity] INT DEFAULT(0) NOT NULL,
CHECK([Quantity] >= 0),
PRIMARY KEY([OrderId], [PartId])
)

CREATE TABLE [PartsNeeded] (
[JobId] INT FOREIGN KEY REFERENCES [Jobs]([JobId]) NOT NULL,
[PartId] INT FOREIGN KEY REFERENCES [Parts]([PartId]) NOT NULL,
[Quantity] INT DEFAULT(1) NOT NULL,
CHECK([Quantity] > 0),
PRIMARY KEY([JobId], [PartId])
)

INSERT INTO Clients([FirstName], [LastName],[Phone])
VALUES
('Teri', 'Ennaco', '570-889-5187'),
('Merlyn', 'Lawler', '201-588-7810'),
('Georgene', 'Montezuma','925-615-5185'),
('Jettie',	'Mconnell', '908-802-3564'),
('Lemuel',	'Latzke', '631-748-6479'),
('Melodie',	'Knipp', '805-690-1682'),
('Candida',	'Corbley', '908-275-8357')

INSERT INTO Parts([SerialNumber], [Description], [Price], [VendorId])
VALUES
('WP8182119', 'Door Boot Seal', 117.86,	2),
('W10780048', 'Suspension Rod', 42.81,	1),
('W10841140', 'Silicone Adhesive',  6.77, 4),
('WPY055980', 'High Temperature Adhesive', 13.94, 3)

UPDATE Jobs
SET MechanicId = 3,
	[Status] = 'In Progress'
WHERE [Status] = 'Pending'

DELETE OrderParts
WHERE OrderId = 19

DELETE Orders
WHERE OrderId = 19

SELECT 
CONCAT(m.FirstName, ' ', m.LastName) AS [Mechanic],
j.[Status] AS [Status],
j.[IssueDate] AS [IssueDate]
FROM Jobs AS j
JOIN Mechanics AS m
ON j.MechanicId = m.MechanicId
ORDER BY m.MechanicId, j.IssueDate, j.JobId

SELECT
CONCAT(c.FirstName, ' ', c.LastName) AS [Client],
DATEDIFF(DAY,[IssueDate], '2017/04/24') AS [Days going],
j.[Status] AS [Status]
FROM Jobs AS j
JOIN Clients AS c
ON j.ClientId = c.ClientId
WHERE [Status] != 'Finished'
ORDER BY [Days going] DESC, c.[ClientId] 

SELECT 
	[Mechanic],
	AVG(SubQuerry.[Average Days]) AS [Average Days] FROM 
(
	SELECT 
		m.MechanicId AS [orderId],
		CONCAT(m.FirstName, ' ', m.LastName) AS [Mechanic],
		DATEDIFF(DAY, j.IssueDate, j.FinishDate) AS [Average Days]
		FROM Jobs AS j
		JOIN Mechanics AS m
		ON j.MechanicId = m.MechanicId
) AS SubQuerry
GROUP BY [Mechanic], orderId
ORDER BY orderId


SELECT 
CONCAT(FirstName, ' ', LastName) AS Mechanic 
FROM Mechanics AS m
WHERE m.MechanicId NOT IN 
( 
SELECT j.MechanicId FROM Jobs AS j
WHERE j.[Status] = 'In Progress'
GROUP BY MechanicId
)
ORDER BY m.MechanicId

SELECT 
j.JobId,
CASE WHEN SUM(p.Price * op.Quantity) IS NULL THEN 0 ELSE SUM(p.Price * op.Quantity) END AS TotalSum
FROM Jobs AS j
LEFT JOIN Orders AS o
ON o.JobId = j.JobId
LEFT JOIN OrderParts AS op
ON o.OrderId = op.OrderId
LEFT JOIN Parts AS p
ON op.PartId = p.PartId
WHERE j.[Status] = 'Finished'
GROUP BY j.JobId
ORDER BY SUM(p.Price * op.Quantity) DESC, j.JobId

GO

CREATE FUNCTION udf_GetCost(@jobId INT)
RETURNS DECIMAL(6,2)
AS
BEGIN 

DECLARE @Result DECIMAL (6,2)
SET @Result = (
SELECT ISNULL(SUM(p.Price),0) FROM Jobs AS j
LEFT JOIN Orders AS o
ON o.JobId = j.JobId
LEFT JOIN OrderParts AS op
ON op.OrderId = o.OrderId
LEFT JOIN Parts AS p
ON op.PartId = p.PartId
WHERE j.JobId = @jobId
GROUP BY j.JobId
)

RETURN @Result
END

GO 

SELECT dbo.udf_GetCost(1)
