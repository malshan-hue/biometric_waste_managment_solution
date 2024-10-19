CREATE TABLE [dbo].[Employee]
(
	[EmployeeId] INT IDENTITY NOT NULL,
	[FullName] NVARCHAR(200) NULL,
	[Email] NVARCHAR(200) NULL,
	[EmployeeCode] NVARCHAR(50) NULL,
	[Phone] NVARCHAR(50) NULL,

	CONSTRAINT [Employee_EmployeeId_PK] PRIMARY KEY ([EmployeeId])	
)
