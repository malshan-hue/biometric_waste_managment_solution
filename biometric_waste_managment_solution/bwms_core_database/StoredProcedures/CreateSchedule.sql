CREATE PROCEDURE [dbo].[CreateSchedule]
	@jsonString NVARCHAR(MAX) = '',
	@executionStatus BIT OUT
	WITH ENCRYPTION
AS
BEGIN
	
	
	INSERT INTO WastePickupSchedule([CustomerId], [ScheduledDate], [WasteTypeEnum], [PickupStatusEnum], [Address], [EstimatedVolume])
	SELECT [CustomerId], [ScheduledDate], [WasteTypeEnum], [PickupStatusEnum], [Address], [EstimatedVolume]
	FROM OPENJSON(@jsonString, '$')
	WITH(
		[CustomerId] INT, 
		[ScheduledDate] DATETIME,
		[WasteTypeEnum] INT, 
		[PickupStatusEnum] INT,
		[Address] NVARCHAR(500) ,
		[EstimatedVolume] DECIMAL(18,2)
	);

	SET @executionStatus = 1;
END
