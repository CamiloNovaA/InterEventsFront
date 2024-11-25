USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_SubscribeForEvent
	@IdEvent INT,
	@IdUser INT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM [dbo].[InterEvents] WHERE [EVT_IdEvent] = @IdEvent)
	BEGIN
		INSERT INTO [dbo].[Attendants]
			([ATN_IdEvent], [ATN_IdUser])
		VALUES
			(@IdEvent, @IdUser)
	END
END