USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_CreateEvent
	@Name VARCHAR(100),
	@Description VARCHAR(MAX),
	@EventDate DATETIME,
	@IdCity INT,
	@Latitude VARCHAR(20),
	@Longitude VARCHAR(20),
	@Address VARCHAR(300),
	@Capacity INT,
	@IdOwner INT
AS
BEGIN
	INSERT INTO [dbo].[InterEvents]
		([EVT_EventName], [EVT_Description], [EVT_Date], 
		[EVT_IdCity], [EVT_Latitude], [EVT_Longitude], 
		[EVT_Capacity], [EVT_IdOwner], [EVT_Address])
	VALUES
		(@Name, @Description, @EventDate,
		@IdCity, @Latitude, @Longitude,
		@Capacity, @IdOwner, @Address)

	SELECT SCOPE_IDENTITY();
END