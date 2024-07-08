dotnet ef migrations add initial --context CustomerDbContext -o "Data\EF\Migrations"
dotnet ef database update --context CustomerDbContext

CREATE PROCEDURE [dbo].[ConfirmRegisteration]
(
@id BIGINT
)
AS
BEGIN

	SET NOCOUNT ON;
	--JUST FOR DEMO PURPOSE


END
