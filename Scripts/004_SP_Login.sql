USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_Login
	@Email VARCHAR(100)
AS
BEGIN
	SELECT
		[USR_IdUser], [USR_Password], [USR_Name] + ' ' + [USR_LastName] AS USR_FullName
	FROM
		[dbo].[Users]
	WHERE
		[USR_Mail] = @Email
END