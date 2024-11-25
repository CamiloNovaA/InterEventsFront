USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_GetEventsById
	@IdUser INT
AS
BEGIN
	SELECT 
		 [EVT_IdEvent], [EVT_EventName], [EVT_Description], [EVT_Date],
		 [EVT_IdCity], [CTY_Name], [EVT_Latitude], [EVT_Longitude], [EVT_Capacity],
		 [EVT_IdOwner], [EVT_CreationDate], [EVT_Address], 
		 CASE WHEN [ATN_IdEvent] IS NULL 
		 THEN 0 ELSE 1 END AS Suscrito
	FROM
		[dbo].[InterEvents] [EVT]
	INNER JOIN [dbo].[Cities] [CTY] ON [CTY].[CTY_IdCity] = [EVT].[EVT_IdCity]
	LEFT JOIN [dbo].[Attendants] [ATN] ON [ATN].[ATN_IdEvent] = [EVT].[EVT_IdEvent] AND [ATN].[ATN_IdUser] = @IdUser
	WHERE 
		[EVT_Date] >= GETDATE() AND 
		[EVT_Active] = 1
END