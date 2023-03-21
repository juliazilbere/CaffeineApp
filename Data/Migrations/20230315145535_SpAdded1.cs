using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
  public partial class SpAdded1 : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      var createProcSql = @"SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE OR ALTER PROCEDURE CaffeineIntakeByUser 
	@UserID int
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @UserName nvarchar(256)
	DECLARE @cnt float = 24
	DECLARE @dt datetime
	DECLARE @dd varchar (10)
	DECLARE @dh int
	DECLARE @ci float = 0
	DECLARE @cc float = 0
	
	SELECT @UserName = UserName
	FROM AspNetUsers
	WHERE Id = @UserID

    CREATE TABLE #Intake (
		UserId bigint,
		UserName nvarchar(256),
		dt varchar (10),
		hrs int,
		CaffeineIntakeNum float)

	WHILE @cnt >= 0
	BEGIN
		SELECT	@dt = GETDATE()- @cnt/24
		SELECT	@dd = Convert(varchar, @dt, 23),
				@dh = DatePart(hour, @dt),
				@cc = 0

		SELECT	@cc = SUM(d.CafeinePer100ml / 100 * dl.ActualSize)
		FROM	[DrinkLog] dl
				INNER JOIN Drinks d ON dl.DrinkId = d.Id
		WHERE	dl.UserId = @UserID 
			    AND Convert(varchar, dl.DrinkTime, 23) = @dd
				AND DatePart(hour, dl.DrinkTime) = @dh
		GROUP BY dl.UserId

		--SELECT @dt, @dd, @dh, @cnt, @cc

		SELECT	@ci = @ci * 0.87 + IsNull (@cc, 0)

		INSERT INTO #Intake (dt, hrs, CaffeineIntakeNum, UserId, UserName)
		VALUES (Convert(varchar, @dt, 23), 
				DatePart(hour, @dt),
				ROUND(@ci,2),
				@UserID,
				@UserName)

		SELECT @cnt = @cnt-1
	END

	SELECT * FROM #Intake
END
GO";
      migrationBuilder.Sql(createProcSql);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      var dropProcSql = "DROP PROC CaffeineIntakeByUser";
      migrationBuilder.Sql(dropProcSql);
    }
  }
}
