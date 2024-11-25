USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_ChangeStateEvent
	@IdEvent INT,
	@Active INT,
	@IdOwner INT
AS
BEGIN
	UPDATE [dbo].[InterEvents]
	SET
		[EVT_Active] = @Active
	WHERE 
		[EVT_IdEvent] = @IdEvent AND
		[EVT_IdOwner] = @IdOwner
END