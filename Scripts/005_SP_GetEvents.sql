USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_GetEvents
AS
BEGIN
	SELECT 
		 [EVT_IdEvent], [EVT_EventName], [EVT_Description], [EVT_Date],
		 [EVT_IdCity], [CTY_Name], [EVT_Latitude], [EVT_Longitude], [EVT_Capacity],
		 [EVT_IdOwner], [EVT_CreationDate], [EVT_Address]
	FROM
		[dbo].[InterEvents]
	INNER JOIN [dbo].[Cities] [CTY] ON [CTY].[CTY_IdCity] = [EVT_IdCity]
	WHERE 
		[EVT_Date] >= GETDATE() AND 
		[EVT_Active] = 1
END