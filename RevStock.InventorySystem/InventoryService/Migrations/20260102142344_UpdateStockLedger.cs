using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStockLedger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StockLedgers",
                table: "StockLedgers");

            migrationBuilder.DropColumn(
                name: "LedgerId",
                table: "StockLedgers");

            migrationBuilder.RenameColumn(
                name: "ReferenceId",
                table: "StockLedgers",
                newName: "StockLedgerId");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "StockLedgers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitCost",
                table: "StockLedgers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockLedgers",
                table: "StockLedgers",
                column: "StockLedgerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StockLedgers",
                table: "StockLedgers");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "StockLedgers");

            migrationBuilder.DropColumn(
                name: "UnitCost",
                table: "StockLedgers");

            migrationBuilder.RenameColumn(
                name: "StockLedgerId",
                table: "StockLedgers",
                newName: "ReferenceId");

            migrationBuilder.AddColumn<Guid>(
                name: "LedgerId",
                table: "StockLedgers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockLedgers",
                table: "StockLedgers",
                column: "LedgerId");
        }
    }
}
