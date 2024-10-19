CREATE TABLE [dbo].[WastePickupSchedule]
(
	[WastePickupScheduleId] INT IDENTITY NOT NULL,
    [CustomerId] INT NULL, 
    [ScheduledDate] DATETIME NULL,
    [WasteTypeEnum] INT NULL, 
    [PickupStatusEnum] INT NULL,
    [Address] NVARCHAR(500)  NULL,
    [EstimatedVolume] FLOAT NULL,
    [DriverId] INT NULL,
    [MapLocation] NVARCHAR(500) NULL,

    CONSTRAINT [PK_WastePickupSchedule] PRIMARY KEY ([WastePickupScheduleId])
)