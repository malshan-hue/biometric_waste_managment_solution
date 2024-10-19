CREATE TABLE [dbo].[User]
(
	[UserId] INT IDENTITY NOT NULL,
	[UserName] NVARCHAR(200) NOT NULL,
	[Password] NVARCHAR(100) NOT NULL,
	[PasswordSalt] NVARCHAR(100) NOT NULL,
	[ActivationCode] INT NOT NULL,
	[CreatedDate] DATETIME NOT NULL,
	[LastLogginDate] DATETIME NOT NULL,
	[IsAuthority] BIT DEFAULT 0,
	[IsActive] BIT DEFAULT 0,
	[IsDeleted] BIT DEFAULT 0

	CONSTRAINT [User_UserId_PK] PRIMARY KEY ([UserId])
)
