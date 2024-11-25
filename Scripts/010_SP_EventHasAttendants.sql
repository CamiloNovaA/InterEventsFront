USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_EventHasAttendants
	@IdEvent INT
AS
BEGIN
	SELECT 
		COUNT(1)
	FROM
		[dbo].[InterEvents] [EVT]
	INNER JOIN [dbo].[Attendants] [ATN] ON [ATN].[ATN_IdEvent] = [EVT_IdEvent]
	WHERE
		[EVT_IdEvent] = @IdEvent
END