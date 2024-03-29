CREATE DATABASE CigarShop

USE CigarShop

-- 1. DDL

CREATE TABLE Sizes (
	[Id] INT PRIMARY KEY IDENTITY,
	[Length] INT NOT NULL
	CHECK([Length] BETWEEN 10 AND 25),
	[RingRange] DECIMAL(18,2) NOT NULL,
	CHECK([RingRange] BETWEEN 1.5 AND 7.5)
)

CREATE TABLE Tastes (
	[Id] INT PRIMARY KEY IDENTITY,
	[TasteType] VARCHAR(20) NOT NULL,
	[TasteStrength] VARCHAR(15) NOT NULL,
	[ImageURL] NVARCHAR(100) NOT NULL
)

CREATE TABLE Brands (
	[Id] INT PRIMARY KEY IDENTITY,
	[BrandName] VARCHAR(30) NOT NULL UNIQUE,
	[BrandDescription] VARCHAR(MAX)
)

CREATE TABLE Cigars (
	[Id] INT PRIMARY KEY IDENTITY,
	[CigarName] VARCHAR(80) NOT NULL,
	[BrandId] INT FOREIGN KEY REFERENCES [Brands]([Id]) NOT NULL,
	[TastId] INT FOREIGN KEY REFERENCES [Tastes]([Id]) NOT NULL,
	[SizeId] INT FOREIGN KEY REFERENCES [Sizes]([Id]) NOT NULL,
	[PriceForSingleCigar] DECIMAL(18,2) NOT NULL,
	[ImageURL] NVARCHAR(100) NOT NULL
)

CREATE TABLE Addresses (
	[Id] INT PRIMARY KEY IDENTITY,
	[Town] VARCHAR(30) NOT NULL,
	[Country] NVARCHAR(30) NOT NULL,
	[Streat] NVARCHAR(100) NOT NULL,
	[ZIP] VARCHAR(20) NOT NULL
)

CREATE TABLE Clients (
	[Id] INT PRIMARY KEY IDENTITY,
	[FirstName] NVARCHAR(30) NOT NULL,
	[LastName] NVARCHAR(30) NOT NULL,
	[Email] NVARCHAR(50) NOT NULL,
	[AddressId] INT FOREIGN KEY REFERENCES [Addresses]([Id]) NOT NULL
)

CREATE TABLE ClientsCigars (
	[ClientId] INT FOREIGN KEY REFERENCES [Clients]([Id]) NOT NULL,
	[CigarId] INT FOREIGN KEY REFERENCES [Cigars]([Id]) NOT NULL,
	PRIMARY KEY ([ClientId], [CigarId])
)


-- 2. Insert

INSERT INTO Cigars (CigarName, BrandId, TastId, SizeId, PriceForSingleCigar, ImageURL)
VALUES
('COHIBA ROBUSTO',	9,	1,	5,	15.50, 'cohiba-robusto-stick_18.jpg'),
('COHIBA SIGLO I',	9,	1,	10,	410.00, 'cohiba-siglo-i-stick_12.jpg'),
('HOYO DE MONTERREY LE HOYO DU MAIRE', 14, 5, 11, 7.50, 'hoyo-du-maire-stick_17.jpg'),
('HOYO DE MONTERREY LE HOYO DE SAN JUAN', 14, 4, 15, 32.00,	'hoyo-de-san-juan-stick_20.jpg'),
('TRINIDAD COLONIALES',	2, 3, 8, 85.21, 'trinidad-coloniales-stick_30.jpg')

INSERT INTO Addresses (Town, Country, Streat, ZIP)
VALUES
('Sofia',	'Bulgaria',	'18 Bul. Vasil levski',	'1000'),
('Athens',	'Greece',	'4342 McDonald Avenue',  '10435'),
('Zagreb',	'Croatia',	'4333 Lauren Drive', '10000')

-- 3. Update

UPDATE Cigars
SET PriceForSingleCigar *= 1.20
WHERE TastId = 1

UPDATE Brands
SET BrandDescription = 'New description'
WHERE BrandDescription IS NULL

-- 4. Delete

DELETE FROM Clients WHERE AddressId IN (SELECT Id FROM Addresses WHERE Country LIKE 'C%')

DELETE FROM Addresses WHERE Country LIKE 'C%'

-- 5. Cigars by Price

SELECT
	CigarName,
	PriceForSingleCigar,
	ImageURL
FROM Cigars
ORDER BY PriceForSingleCigar, CigarName DESC

-- 6. Cigars by Taste

SELECT 
	c.Id,
	c.CigarName,
	c.PriceForSingleCigar,
	t.TasteType,
	t.TasteStrength
FROM Cigars AS c
JOIN Tastes AS t
ON c.TastId = t.Id
WHERE t.TasteType IN ('Earthy', 'Woody')
ORDER BY PriceForSingleCigar DESC

-- 7. Clients without Cigars

SELECT
c.Id,
CONCAT(c.FirstName, ' ', c.LastName) AS ClientName,
c.Email
FROM Clients AS c
LEFT JOIN ClientsCigars AS cc ON c.Id = cc.ClientId
WHERE CigarId IS NULL
ORDER BY ClientName

-- 8. First 5 Cigars

SELECT TOP(5) 
	c.CigarName,
	c.PriceForSingleCigar,
	c.ImageURL
FROM Cigars AS c
JOIN Sizes AS s ON c.SizeId = s.Id
WHERE s.[Length] >= 12 AND (c.CigarName LIKE '%ci%' OR c.PriceForSingleCigar > 50) AND s.RingRange > 2.55
ORDER BY c.CigarName, c.PriceForSingleCigar DESC

-- 9. Clients with ZIP Codes

SELECT
	[Full Name],
	Country,
	ZIP,
	CONCAT('$', MAX(SubQuerry.PriceForSingleCigar)) AS CigarPrice
FROM 
(
SELECT 
	CONCAT(c.[FirstName], ' ', c.[LastName]) AS [Full Name],
	a.Country,
	a.ZIP,
	ci.PriceForSingleCigar
FROM Addresses AS a
JOIN Clients AS c ON a.Id = c.AddressId
JOIN ClientsCigars AS cc ON c.Id = cc.ClientId
JOIN Cigars AS ci ON cc.CigarId = ci.Id
WHERE ZIP NOT LIKE '%[^0-9]%'
) AS SubQuerry
GROUP BY [Full Name], Country, ZIP
ORDER BY [Full Name]

-- 10 Cigars by Size
SELECT
LastName,
AVG(SubQuerry.[Length]) AS [CigarLength],
CEILING(AVG(SubQuerry.RingRange)) AS [CiagrRingRange]
FROM
(
SELECT c.LastName, s.[Length], s.RingRange FROM Clients AS c
JOIN ClientsCigars AS cc ON c.Id = cc.ClientId
JOIN Cigars AS ci ON cc.CigarId = ci.Id
JOIN Sizes AS s ON ci.SizeId = s.Id
) AS SubQuerry
GROUP BY LastName
ORDER BY [CigarLength] DESC

-- 11. Client with Cigars

GO

CREATE FUNCTION udf_ClientWithCigars(@name NVARCHAR(30)) 
RETURNS INT
BEGIN
	DECLARE @result INT

	SET @result = (SELECT COUNT(*) FROM Clients AS c
	JOIN ClientsCigars AS cc ON c.Id = cc.ClientId
	JOIN Cigars AS ci ON cc.CigarId = ci.Id
	WHERE FirstName = @name)

	RETURN @result
END

SELECT dbo.udf_ClientWithCigars('Betty')

-- 12. Search for Cigar with Specific Taste

GO

CREATE PROCEDURE usp_SearchByTaste(@taste VARCHAR(20))
AS
BEGIN
	SELECT 
	ci.CigarName,
	CONCAT('$', ci.PriceForSingleCigar) AS Price,
    t.TasteType,
	b.BrandName,
	CONCAT(s.[Length], ' ', 'cm') AS CigarLength,
	CONCAT(s.RingRange, ' ', 'cm') AS CigarRingRange
	FROM Cigars AS ci
	JOIN Tastes AS t ON ci.TastId = t.Id
	JOIN Brands AS b ON ci.BrandId = b.Id
	JOIN Sizes AS s ON ci.SizeId = s.Id
	WHERE t.TasteType = @taste
	ORDER BY s.[Length], s.RingRange DESC
END

EXEC usp_SearchByTaste 'Woody'