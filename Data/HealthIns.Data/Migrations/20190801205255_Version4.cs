using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthIns.Data.Migrations
{
    public partial class Version4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "smoker",
                table: "Persons",
                newName: "Smoker");

            migrationBuilder.RenameColumn(
                name: "sex",
                table: "Persons",
                newName: "Sex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Smoker",
                table: "Persons",
                newName: "smoker");

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "Persons",
                newName: "sex");
        }
    }
}
