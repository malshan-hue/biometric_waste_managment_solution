CREATE PROCEDURE [dbo].[CreateUser]
	@jsonString NVARCHAR(MAX),
	@executionStatus BIT OUT
	WITH ENCRYPTION
AS
BEGIN

	INSERT INTO [User]([UserName], [Password], [PasswordSalt], [ActivationCode], [CreatedDate], [LastLogginDate], [IsAuthority])
	SELECT [UserName], [Password], [PasswordSalt], [ActivationCode], GETUTCDATE(), GETUTCDATE(), [IsAuthority]
	FROm OPENJSON(@jsonString, '$')
	WITH(
		[UserName] NVARCHAR(200),
		[Password] NVARCHAR(100),
		[PasswordSalt] NVARCHAR(100),
		[ActivationCode] INT,
		[IsAuthority] BIT
	);

	SET @executionStatus = 1;

END
