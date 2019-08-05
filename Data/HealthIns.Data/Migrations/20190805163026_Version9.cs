using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthIns.Data.Migrations
{
    public partial class Version9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Distributors",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationId",
                table: "Distributors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Distributors",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DistributorId",
                table: "Contracts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_OrganizationId",
                table: "Distributors",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_UserId",
                table: "Distributors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_DistributorId",
                table: "Contracts",
                column: "DistributorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Distributors_DistributorId",
                table: "Contracts",
                column: "DistributorId",
                principalTable: "Distributors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Distributors_Organizations_OrganizationId",
                table: "Distributors",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Distributors_AspNetUsers_UserId",
                table: "Distributors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Distributors_DistributorId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Distributors_Organizations_OrganizationId",
                table: "Distributors");

            migrationBuilder.DropForeignKey(
                name: "FK_Distributors_AspNetUsers_UserId",
                table: "Distributors");

            migrationBuilder.DropIndex(
                name: "IX_Distributors_OrganizationId",
                table: "Distributors");

            migrationBuilder.DropIndex(
                name: "IX_Distributors_UserId",
                table: "Distributors");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_DistributorId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Distributors");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Distributors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Distributors");

            migrationBuilder.DropColumn(
                name: "DistributorId",
                table: "Contracts");
        }
    }
}
