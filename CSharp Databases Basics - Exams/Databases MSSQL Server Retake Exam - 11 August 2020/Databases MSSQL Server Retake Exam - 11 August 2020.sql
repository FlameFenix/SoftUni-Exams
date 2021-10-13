CREATE DATABASE Bakery

USE Bakery

-- 1. DDL

CREATE TABLE Countries (
[Id] INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(50) UNIQUE
)

CREATE TABLE Customers (
[Id] INT PRIMARY KEY IDENTITY,
[FirstName] NVARCHAR(25),
[LastName] NVARCHAR(25),
[Gender] CHAR(1)
CHECK([Gender] IN ('M', 'F')),
[Age] INT,
[PhoneNumber] CHAR(10),
[CountryId] INT FOREIGN KEY REFERENCES [Countries](Id)
)

CREATE TABLE Products (
[Id] INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(25),
[Description] NVARCHAR(250),
[Recipe] NVARCHAR(MAX),
[Price] DECIMAL(18,2)
CHECK([Price] >= 0)
)

CREATE TABLE Feedbacks (
[Id] INT PRIMARY KEY IDENTITY,
[Description] NVARCHAR(255),
[Rate] DECIMAL(4,2),
[ProductId] INT FOREIGN KEY REFERENCES [Products]([Id]),
[CustomerId] INT FOREIGN KEY REFERENCES [Customers]([Id])
)

CREATE TABLE Distributors (
[Id] INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(25) UNIQUE,
[AddressText] NVARCHAR(30),
[Summary] NVARCHAR(200),
[CountryId] INT FOREIGN KEY REFERENCES [Countries]([Id])
)

CREATE TABLE Ingredients (
[Id] INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(30),
[Description] NVARCHAR(200),
[OriginCountryId] INT FOREIGN KEY REFERENCES [Countries]([Id]),
[DistributorId] INT FOREIGN KEY REFERENCES [Distributors]([Id])
)

CREATE TABLE ProductsIngredients (
[ProductId] INT FOREIGN KEY REFERENCES [Products]([Id]) NOT NULL,
[IngredientId] INT FOREIGN KEY REFERENCES [Ingredients]([Id]) NOT NULL,
PRIMARY KEY ([ProductId], [IngredientId])
)

-- 2. Insert

INSERT INTO [Distributors] ([Name], [CountryId], [AddressText], [Summary])
VALUES
('Deloitte & Touche	', 2, '6 Arch St #9757',	'Customizable neutral traveling'),
('Congress Title', 13, '58 Hancock St',	'Customer loyalty'),
('Kitchen People', 1, '3 E 31st St #77',	'Triple-buffered stable delivery'),
('General Color Co Inc', 21, '6185 Bohn St #72',	'Focus group'),
('Beck Corporation', 23 ,'21 E 64th Ave',	'Quality-focused 4th generation hardware')

INSERT INTO [Customers] ([FirstName], [LastName], [Age], [Gender], [PhoneNumber], [CountryId])
VALUES
('Francoise', 'Rautenstrauch',	15,	'M', '0195698399', 5),
('Kendra', 'Loud',	22,	'F','0063631526', 11),
('Lourdes', 'Bauswell',	50	,'M', '0139037043', 8),
('Hannah', 'Edmison',	18	,'F', '0043343686', 1),
('Tom',	'Loeza', 31, 'M', '0144876096', 23),
('Queenie', 'Kramarczyk', 30 , 'F' , '0064215793', 29),
('Hiu', 'Portaro', 25, 'M', '0068277755', 16),
('Josefa', 'Opitz',	43 ,'F', '0197887645', 17)

-- 3. Update 
UPDATE Ingredients
SET [DistributorId] = 35
WHERE [Name] IN ('Bay Leaf', 'Paprika', 'Poppy')

UPDATE Ingredients
SET [OriginCountryId] = 14
WHERE [OriginCountryId] = 8

-- 4. Delete

DELETE FROM Feedbacks
WHERE [ProductId] = 5 OR [CustomerId] = 14

-- 5. Products by Price

SELECT [Name], [Price], [Description] FROM Products
ORDER BY [Price] DESC, [Name]

-- 6. Negative Feedback

SELECT f.[ProductId],
	   f.[Rate],
	   f.[Description],
	   f.[CustomerId],
	   c.[Age],
	   c.[Gender]
FROM Feedbacks AS f
JOIN Customers AS c
ON f.CustomerId = c.Id
WHERE f.[Rate] < 5.00
ORDER BY ProductId DESC, f.[Rate]

-- 7. Customers without Feedback

SELECT CONCAT(c.FirstName, ' ', c.LastName) AS [CustomerName], c.PhoneNumber, c.Gender FROM Customers AS c
WHERE c.Id NOT IN
(
SELECT f.CustomerId FROM Feedbacks AS f
WHERE f.CustomerId IN (SELECT Id FROM Customers)
)

-- 8. Customers by Criteria

SELECT cm.FirstName, cm.Age, cm.PhoneNumber FROM Customers AS cm
JOIN Countries AS ct
ON cm.CountryId = ct.Id
WHERE cm.Age >= 21 AND cm.[FirstName] LIKE '%an%' OR cm.PhoneNumber LIKE '%38' AND ct.[Name] != 'Greece'
ORDER BY cm.[FirstName], cm.PhoneNumber DESC

-- 9. Middle Range Distributors

SELECT d.[Name], i.[Name], p.[Name], AVG(f.[Rate]) FROM Distributors AS d
LEFT JOIN Ingredients AS i
ON d.Id = i.DistributorId
LEFT JOIN ProductsIngredients AS ps
ON i.Id = ps.IngredientId
LEFT JOIN Products AS p
ON Ps.ProductId = p.Id
LEFT JOIN Feedbacks AS f
ON p.Id = f.ProductId
GROUP BY d.[Name], i.[Name], p.[Name]
HAVING AVG(f.[Rate]) BETWEEN 5 AND 8
ORDER BY d.[Name], i.[Name], p.[Name]

-- 10. Country Representative (unfinished) 

SELECT c.[Name], d.[Name], COUNT(i.Id) AS [Count] FROM Countries AS c
LEFT JOIN Distributors AS d
ON c.Id = d.CountryId
LEFT JOIN Ingredients AS i
ON d.Id = i.DistributorId
GROUP BY c.[Name], d.[Name]
ORDER BY c.[Name], d.[Name]

-- 11. Customers with Countries

CREATE VIEW v_UserWithCountries AS
SELECT 
	CONCAT(cs.[FirstName], ' ', cs.[LastName]) AS [CustomerName],
	cs.[Age],
	cs.[Gender],
	ct.[Name] AS CountryName
FROM Customers AS cs
JOIN Countries AS ct
ON cs.CountryId = ct.Id

SELECT TOP(5) * FROM dbo.v_UserWithCountries
ORDER BY Age
