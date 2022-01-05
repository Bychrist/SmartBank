using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartBank.Migrations
{
    public partial class accountModelAdjust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountNo",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountNo",
                table: "Accounts",
                column: "AccountNo",
                unique: true,
                filter: "[AccountNo] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountNo",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNo",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
