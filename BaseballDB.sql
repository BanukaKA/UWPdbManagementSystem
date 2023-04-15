--Name: Banuka Kumara Ambegoda
--Lab 4/5
--S22 PROG 1198-01
--Date: 2022-06-20

USE master
GO

--Start
DROP DATABASE IF EXISTS BaseballLeague
GO
CREATE DATABASE BaseballLeague
GO
USE BaseballLeague
GO

--Creating a table
CREATE TABLE PLAYER
(
	playerID             INT IDENTITY(1,1) PRIMARY KEY,
	playerFirstName	     VARCHAR(30)	   NOT NULL,
	playerLastName	     VARCHAR(40)	   NOT NULL,
	playerBattingAverage DECIMAL(4,3)      DEFAULT 0.000,
	playerRunsScored     INT               DEFAULT 0,
	CONSTRAINT CHK_Player CHECK (playerBattingAverage >= 0 AND playerRunsScored >= 0)
)
GO

--Inserting data into the table
INSERT INTO PLAYER 
	(playerFirstName, playerLastName, playerBattingAverage, playerRunsScored) 
VALUES 
	('Howard','Stark',0.785 ,1432),
	('Bruce','Wayne',0.322, 2345),
	('Tony','Stark',0.879, 5432),
	('Peter','Parker',0.312, 3243),
	('Wade','Wilson',0.376, 2432),
	('John','Watson',0.456, 2932);
GO

--Procedure to select data from table
CREATE OR ALTER PROCEDURE usp_PlayerSelectByID
	@playerLastName VARCHAR(40) = '',
	@playerBattingAvgLow VARCHAR(10) = '',
	@playerBattingAvgHigh VARCHAR(10) = ''
AS
	SET NOCOUNT ON

	IF (@playerBattingAvgLow = '') SET @playerBattingAvgLow = '0.000'
	IF (@playerBattingAvgHigh = '') SET @playerBattingAvgHigh = '1.000'

	DECLARE @baLow DECIMAL(4,3) =  CAST(@playerBattingAvgLow AS DECIMAL(4,3))
	DECLARE @baHigh DECIMAL(4,3) =  CAST(@playerBattingAvgHigh AS DECIMAL(4,3))

	SELECT playerID, playerFirstName, playerLastName, playerBattingAverage, playerRunsScored 
	FROM PLAYER
	WHERE ((playerLastName = @playerLastName) OR (@playerLastName = '')) AND (playerBattingAverage BETWEEN @baLow AND @baHigh)

GO

