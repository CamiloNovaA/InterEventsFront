USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_DeleteEvent
	@IdEvent INT,
	@IdOwner INT
AS
BEGIN
	DELETE 
		[dbo].[InterEvents]
	WHERE 
		[EVT_IdEvent] = @IdEvent AND
		[EVT_IdOwner] = @IdOwner
END