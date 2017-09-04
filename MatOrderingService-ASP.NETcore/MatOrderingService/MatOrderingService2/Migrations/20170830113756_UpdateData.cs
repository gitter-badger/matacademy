using Microsoft.EntityFrameworkCore.Migrations;

namespace MatOrderingService2.Migrations
{
    public partial class UpdateData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderCode",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 9);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderCode",
                table: "Orders",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
