USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_ValidateQuoteEvent
	@IdEvent INT
AS
BEGIN
	SELECT 
		CASE 
			WHEN [EVT].[EVT_Capacity] = COUNT([ATN].[ATN_IdUser]) THEN 1
			ELSE 0
		END AS Comparacion
	FROM 
		[dbo].[InterEvents] [EVT]
	LEFT JOIN 
		[dbo].[Attendants] [ATN] ON [EVT].[EVT_IdEvent] = [ATN].[ATN_IdEvent]
	WHERE [EVT].[EVT_IdEvent] = @IdEvent
	GROUP BY 
		[EVT].[EVT_Capacity]
END