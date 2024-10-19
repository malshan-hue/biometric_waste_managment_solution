CREATE TABLE [dbo].[EmployeeDevice]
(
	[EmployeeDeviceId] INT IDENTITY NOT NULL,
    [EmployeeId] INT NULL,
    [DeviceId] INT NULL,
    [IsActive] BIT NULL,

    CONSTRAINT [PK_EmployeeDevice] PRIMARY KEY ([EmployeeDeviceId]),
    CONSTRAINT [FK_EmployeeDevice_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([EmployeeId]) ON DELETE CASCADE    
)