USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_UnsubscribeForEvent
	@IdEvent INT,
	@IdUser INT
AS
BEGIN
	DELETE [dbo].[Attendants]
	WHERE 
		[ATN_IdEvent] = @IdEvent AND
		[ATN_IdUser] = @IdUser
END