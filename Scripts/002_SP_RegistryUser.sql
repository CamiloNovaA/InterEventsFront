USE InterEventsDB
GO
CREATE OR ALTER PROCEDURE SP_RegistryUser
	@Name VARCHAR(100),
	@LastName VARCHAR(100),
	@Email VARCHAR(100),
	@Password VARCHAR(200)
AS
BEGIN
	INSERT INTO [dbo].[Users]
		(USR_Name, USR_LastName, USR_Mail, USR_Password)
	VALUES
		(@Name, @LastName, @Email, @Password)

	SELECT SCOPE_IDENTITY();
END