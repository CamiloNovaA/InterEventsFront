USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_EditEvent
	@IdEvent INT,
	@IdOwner INT,
	@EventDate DATETIME,
	@IdCity INT,
	@Latitude VARCHAR(20),
	@Longitude VARCHAR(20),
	@Capacity INT
AS
BEGIN
	UPDATE [dbo].[InterEvents]
	SET
		[EVT_Date] = @EventDate, 
		[EVT_IdCity] = @IdCity, 
		[EVT_Latitude] = @Latitude, 
		[EVT_Longitude] = @Longitude, 
		[EVT_Capacity] = @Capacity
	WHERE 
		[EVT_IdEvent] = @IdEvent AND
		[EVT_IdOwner] = @IdOwner
END