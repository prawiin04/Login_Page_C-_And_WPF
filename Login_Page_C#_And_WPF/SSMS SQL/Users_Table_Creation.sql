USE [Login_DB]
-- Create a new table called 'Customers' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
DROP TABLE dbo.Users
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Users
(
   FirstName [NVARCHAR](50)  NOT NULL,
   LastName  [NVARCHAR](50)  NOT NULL,
   Email     [NVARCHAR](50)  NOT NULL,
   Password  [NVARCHAR](50) NOT NULL,
   Address   [NVARCHAR](100) NOT NULL
);
GO