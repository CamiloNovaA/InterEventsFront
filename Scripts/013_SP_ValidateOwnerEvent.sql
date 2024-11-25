USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_ValidateOwnerEvent
	@IdUser INT,
	@IdEvent INT
AS
BEGIN
	SELECT 
		COUNT(1)
	FROM
		[dbo].[InterEvents]
	WHERE
		[EVT_IdEvent] = @IdEvent AND
		[EVT_IdOwner] = @IdUser
END