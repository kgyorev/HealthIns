using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthIns.Data.Migrations
{
    public partial class Version10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Premiums_MoneyIns_MoneyInId",
                table: "Premiums");

            migrationBuilder.DropIndex(
                name: "IX_Premiums_MoneyInId",
                table: "Premiums");

            migrationBuilder.AlterColumn<long>(
                name: "MoneyInId",
                table: "Premiums",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Premiums_MoneyInId",
                table: "Premiums",
                column: "MoneyInId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Premiums_MoneyIns_MoneyInId",
                table: "Premiums",
                column: "MoneyInId",
                principalTable: "MoneyIns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Premiums_MoneyIns_MoneyInId",
                table: "Premiums");

            migrationBuilder.DropIndex(
                name: "IX_Premiums_MoneyInId",
                table: "Premiums");

            migrationBuilder.AlterColumn<long>(
                name: "MoneyInId",
                table: "Premiums",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Premiums_MoneyInId",
                table: "Premiums",
                column: "MoneyInId");

            migrationBuilder.AddForeignKey(
                name: "FK_Premiums_MoneyIns_MoneyInId",
                table: "Premiums",
                column: "MoneyInId",
                principalTable: "MoneyIns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
