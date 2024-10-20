CREATE PROCEDURE [dbo].[UpdatePaymentStauts]
	@jsonString NVARCHAR(MAX) = '',
	@executionStatus BIT OUT
	WITH ENCRYPTION
AS
BEGIN
	
	UPDATE Payment SET PaymentStatus = 'Paid' WHERE PaymentId = (SELECT [PaymentId] FROM OPENJSON(@jsonString, '$')WITH([PaymentId] INT))
	SET @executionStatus = 1;
END