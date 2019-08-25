using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthIns.Data.Migrations
{
    public partial class Version12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClaimActivities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    OperationAmount = table.Column<double>(nullable: false),
                    ContractId = table.Column<long>(nullable: true),
                    ClaimDate = table.Column<DateTime>(nullable: false),
                    ClaimEventType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimActivities_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimActivities_ContractId",
                table: "ClaimActivities",
                column: "ContractId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimActivities");
        }
    }
}
