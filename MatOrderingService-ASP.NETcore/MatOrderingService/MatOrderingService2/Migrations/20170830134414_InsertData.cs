using Microsoft.EntityFrameworkCore.Migrations;

namespace MatOrderingService2.Migrations
{
    public partial class InsertData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Products]
           ([Code]
           ,[Name])
     VALUES
            ('A1145', 'EntityFramework' ),
            ('A1146', 'Ado.Net' ),
            ('A1147', 'Swagger' ),
            ('A1148', 'Postman' ),
            ('A1149', 'Slack' )");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
