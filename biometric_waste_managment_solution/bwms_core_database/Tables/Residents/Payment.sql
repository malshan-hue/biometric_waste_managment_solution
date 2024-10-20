CREATE TABLE [dbo].[Payment]
(
	[PaymentId] INT IDENTITY NOT NULL,
    [CustomerId] INT NULL,
    [PaymentNumber] NVARCHAR(50) NULL,
    [Month] NVARCHAR(50) NULL,
    [Amount] DECIMAL(18, 2) NULL,
    [Discount] DECIMAL(18, 2) NULL,
    [TotalPayable] DECIMAL(18, 2) NULL,
    [PaymentStatus] NVARCHAR(100) NULL,

    CONSTRAINT [PK_Payment] PRIMARY KEY ([PaymentId]),
    CONSTRAINT [FK_Payment_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer]([CustomerId])
)
