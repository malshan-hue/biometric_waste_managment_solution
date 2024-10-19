CREATE TABLE [dbo].[Customer]
(
	[CustomerId] INT IDENTITY NOT NULL,
    [CustomerName] NVARCHAR(200) NULL,
    [CustomerEmail] NVARCHAR(200) NULL,
    [CustomerPhone] NVARCHAR(50) NULL,
    [CustomerLocationLatitude] FLOAT NULL,
    [CustomerLocationLongitude] FLOAT NULL,

    CONSTRAINT [PK_Customer] PRIMARY KEY ([CustomerId])
)
