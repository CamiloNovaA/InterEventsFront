USE InterEventsDB
GO
CREATE PROCEDURE SP_ValidateEmail
	@Email VARCHAR(100)
AS
BEGIN
	SELECT 
		COUNT(1)
	FROM
		[dbo].[Users]
	WHERE
		[USR_Mail] = @Email
END