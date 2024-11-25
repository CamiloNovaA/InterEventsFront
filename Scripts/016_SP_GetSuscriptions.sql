USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_GetSuscriptions
	@IdUser NUMERIC
AS
BEGIN
	SELECT 
		 [EVT_IdEvent]
	FROM
		[dbo].[InterEvents] [EVT]
	INNER JOIN [dbo].[Attendants] [ATN] ON [ATN].[ATN_IdEvent] = [EVT].[EVT_IdEvent]
	WHERE 
		[ATN].[ATN_IdUser] = @IdUser
END