USE master
GO
IF NOT EXISTS (
   SELECT name
   FROM sys.databases
   WHERE name = N'Login_DB'
)
CREATE DATABASE [Login_DB]
GO