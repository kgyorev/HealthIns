using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthIns.Data.Migrations
{
    public partial class Version3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sex",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "smoker",
                table: "Persons",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sex",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "smoker",
                table: "Persons");
        }
    }
}
