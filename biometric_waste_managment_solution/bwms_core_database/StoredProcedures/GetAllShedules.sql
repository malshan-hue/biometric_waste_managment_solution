CREATE PROCEDURE [dbo].[GetAllShedules]
AS
BEGIN

	SELECT * FROM WastePickupSchedule FOR JSON PATH

END
