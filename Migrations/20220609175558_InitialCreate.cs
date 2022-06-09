using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MegaDesk.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryType",
                columns: table => new
                {
                    DeliveryTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeliveryName = table.Column<string>(type: "TEXT", nullable: false),
                    PriceUnder1000 = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceBetween1000And2000 = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceOver2000 = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryType", x => x.DeliveryTypeId);
                });

            migrationBuilder.CreateTable(
                name: "DesktopMaterial",
                columns: table => new
                {
                    DesktopMaterialId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DesktopMaterialName = table.Column<string>(type: "TEXT", nullable: false),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesktopMaterial", x => x.DesktopMaterialId);
                });

            migrationBuilder.CreateTable(
                name: "Desk",
                columns: table => new
                {
                    DeskId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Width = table.Column<decimal>(type: "TEXT", nullable: false),
                    Depth = table.Column<decimal>(type: "TEXT", nullable: false),
                    NumOfDrawers = table.Column<int>(type: "INTEGER", nullable: false),
                    DesktopMaterialId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desk", x => x.DeskId);
                    table.ForeignKey(
                        name: "FK_Desk_DesktopMaterial_DesktopMaterialId",
                        column: x => x.DesktopMaterialId,
                        principalTable: "DesktopMaterial",
                        principalColumn: "DesktopMaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeskQuote",
                columns: table => new
                {
                    DeskQuoteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: false),
                    QuoteDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    QuotePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    DeskId = table.Column<int>(type: "INTEGER", nullable: false),
                    DeliveryTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeskQuote", x => x.DeskQuoteId);
                    table.ForeignKey(
                        name: "FK_DeskQuote_DeliveryType_DeliveryTypeId",
                        column: x => x.DeliveryTypeId,
                        principalTable: "DeliveryType",
                        principalColumn: "DeliveryTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeskQuote_Desk_DeskId",
                        column: x => x.DeskId,
                        principalTable: "Desk",
                        principalColumn: "DeskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Desk_DesktopMaterialId",
                table: "Desk",
                column: "DesktopMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_DeskQuote_DeliveryTypeId",
                table: "DeskQuote",
                column: "DeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeskQuote_DeskId",
                table: "DeskQuote",
                column: "DeskId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeskQuote");

            migrationBuilder.DropTable(
                name: "DeliveryType");

            migrationBuilder.DropTable(
                name: "Desk");

            migrationBuilder.DropTable(
                name: "DesktopMaterial");
        }
    }
}
