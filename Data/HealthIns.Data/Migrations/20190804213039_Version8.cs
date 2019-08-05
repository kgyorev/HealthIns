using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthIns.Data.Migrations
{
    public partial class Version8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Distributors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoneyIns",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    OperationAmount = table.Column<double>(nullable: false),
                    ContractId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyIns_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Premiums",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    OperationAmount = table.Column<double>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ContractId = table.Column<long>(nullable: true),
                    MoneyInId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premiums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Premiums_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Premiums_MoneyIns_MoneyInId",
                        column: x => x.MoneyInId,
                        principalTable: "MoneyIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyIns_ContractId",
                table: "MoneyIns",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Premiums_ContractId",
                table: "Premiums",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Premiums_MoneyInId",
                table: "Premiums",
                column: "MoneyInId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Distributors");

            migrationBuilder.DropTable(
                name: "Premiums");

            migrationBuilder.DropTable(
                name: "MoneyIns");
        }
    }
}
