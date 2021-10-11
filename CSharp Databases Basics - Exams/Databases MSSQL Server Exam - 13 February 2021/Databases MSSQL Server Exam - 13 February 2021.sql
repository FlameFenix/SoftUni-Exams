CREATE DATABASE [Bitbucket]

USE [Bitbucket]

-- 1.DLL

CREATE TABLE [Users](
 [Id] INT PRIMARY KEY IDENTITY,
 [Username] VARCHAR(30) NOT NULL,
 [Password] VARCHAR(30) NOT NULL,
 [Email] VARCHAR(50) NOT NULL
)

CREATE TABLE [Repositories] (
[Id] INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE [RepositoriesContributors] (
[RepositoryId] INT FOREIGN KEY REFERENCES [Repositories]([Id]) NOT NULL,
[ContributorId] INT FOREIGN KEY REFERENCES [Users]([Id]) NOT NULL,
PRIMARY KEY([RepositoryId], [ContributorId])
)

CREATE TABLE [Issues] ( 
[Id] INT PRIMARY KEY IDENTITY,
[Title] VARCHAR(255) NOT NULL,
[IssueStatus] CHAR(6) NOT NULL,
[RepositoryId] INT FOREIGN KEY REFERENCES [Repositories]([Id]) NOT NULL,
[AssigneeId] INT FOREIGN KEY REFERENCES [Users]([Id]) NOT NULL
)

CREATE TABLE [Commits] (
[Id] INT PRIMARY KEY IDENTITY,
[Message] VARCHAR(255) NOT NULL,
[IssueId] INT FOREIGN KEY REFERENCES [Issues]([Id]),
[RepositoryId] INT FOREIGN KEY REFERENCES [Repositories]([Id]) NOT NULL,
[ContributorId] INT FOREIGN KEY REFERENCES Users([Id]) NOT NULL,
)

CREATE TABLE [Files] (
[Id] INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(100) NOT NULL,
[Size] DECIMAL(10,2) NOT NULL,
[ParentId] INT FOREIGN KEY REFERENCES [Files]([Id]),
[CommitId] INT FOREIGN KEY REFERENCES [Commits]([Id]) NOT NULL
)

-- 2.Insert

INSERT INTO [Files] ([Name], [Size], [ParentId], [CommitId])
VALUES
('Trade.idk', 2598.0, 1, 1),
('menu.net', 9238.31, 2, 2),
('Administrate.soshy', 1246.93 ,3 ,3),
('Controller.php', 7353.15, 4 ,4),
('Find.java', 9957.86 ,5 ,5),
('Controller.json',	14034.87, 3, 6),
('Operate.xix',	7662.92, 7, 7)

INSERT INTO Issues ([Title], [IssueStatus], [RepositoryId], [AssigneeId])
VALUES
('Critical Problem with HomeController.cs file', 'open',1, 4),
('Typo fix in Judge.html', 'open',4, 3),
('Implement documentation for UsersService.cs',	'closed',8, 2),
('Unreachable code in Index.cs', 'open',9, 8)

-- 3. Update

UPDATE Issues
SET IssueStatus = 'closed'
WHERE AssigneeId = 6

--4. Delete

DELETE RepositoriesContributors
WHERE  RepositoryId = (
SELECT Id FROM Repositories AS r
WHERE r.[Name] = 'Softuni-Teamwork')

DELETE Issues
WHERE RepositoryId = (
SELECT Id FROM Repositories AS r
WHERE r.[Name] = 'Softuni-Teamwork')

-- 5. Commits

SELECT [Id], [Message], [RepositoryId], [ContributorId] FROM Commits
ORDER BY [Id], [Message], [RepositoryId], [ContributorId]

-- 6. Front-end

SELECT [Id], [Name], [Size] FROM Files
WHERE Size > 1000 AND [Name] LIKE '%html%'
ORDER BY Size DESC, Id, [Name]

-- 7. Issue Assignment

SELECT i.[Id], CONCAT(u.[UserName], ' : ', i.[Title]) FROM Issues AS i
JOIN Users AS u ON i.[AssigneeId] = u.[Id]
ORDER BY i.[Id] DESC, i.[AssigneeId]

-- 8. Single Files

SELECT [Id], [Name], CONCAT([Size], 'KB') AS [Size] FROM Files 
WHERE Id NOT IN ( 
SELECT Id FROM Files AS f
WHERE f.Id IN (SELECT ParentId FROM Files)
)
ORDER BY [Id], [Name], [Size]


-- 9. Commits in Repositories

SELECT TOP (5) rp.Id, rp.[Name], COUNT(rp.Id) FROM Repositories AS rp
JOIN RepositoriesContributors AS rc
ON rp.Id = rc.RepositoryId
JOIN Commits AS cm ON rp.Id = cm.RepositoryId 
GROUP BY rp.Id, rp.[Name]
ORDER BY COUNT(rp.Id) DESC, rp.Id, rp.[Name]

-- 10. Average Size

SELECT u.Username, AVG(f.Size) FROM Users AS u
JOIN Commits AS cm
ON u.Id = cm.ContributorId
JOIN Files AS f
ON cm.Id = f.CommitId
GROUP BY u.Username
ORDER BY AVG(f.Size) DESC, u.Username

-- 11. All User Commits

CREATE FUNCTION udf_AllUserCommits(@username VARCHAR(30))
RETURNS INT
BEGIN
DECLARE @Count AS INT
IF( NOT EXISTS (SELECT 1 FROM Users WHERE Username = @username)) RETURN 0
SET @Count = (SELECT COUNT(u.Username) FROM Commits AS c
JOIN Users AS u
ON c.ContributorId = u.Id
WHERE u.Username = @username
GROUP BY u.Username)

RETURN @Count
END

SELECT dbo.udf_AllUserCommits('UnderSinduxrein')

-- 12. Search for Files
CREATE PROCEDURE usp_SearchForFiles(@fileExtension VARCHAR(20))
AS
BEGIN
SELECT f.Id, f.[Name], CONCAT(f.Size, 'KB') AS Size FROM Files AS f
WHERE f.[Name] LIKE ('%' + @fileExtension + '%')
ORDER BY f.Id, f.[Name], f.Size DESC
END

EXEC usp_SearchForFiles 'txt'
