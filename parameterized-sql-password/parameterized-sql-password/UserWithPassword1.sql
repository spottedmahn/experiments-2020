DECLARE @blah varchar(100) = $(Password)
CREATE USER [UserWithPassword1]
	WITH PASSWORD = @blah

GO

GRANT CONNECT TO [UserWithPassword1]