using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthIns.Data.Migrations
{
    public partial class Version5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PersonId",
                table: "Contracts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PersonId",
                table: "Contracts",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Persons_PersonId",
                table: "Contracts",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Persons_PersonId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_PersonId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Contracts");
        }
    }
}
