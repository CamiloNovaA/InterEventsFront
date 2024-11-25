USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_ValidateLimitEventsForAttendant
	@IdUser INT
AS
BEGIN
	SELECT 
		COUNT(*)
	FROM
		[dbo].[Attendants]
	WHERE
		[ATN_IdUser]= @IdUser
END