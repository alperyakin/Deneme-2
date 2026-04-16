using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deneme2.Services.StockService.Persistence.EntityFrameworkCore.Migrations.ApplicationWrite;

/// <inheritdoc />
public partial class InitStockWrite : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "stocks",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                quantity = table.Column<int>(type: "integer", nullable: false),
                row_version = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                created_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                updated_by = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
            },
            constraints: table => table.PrimaryKey("pk_stocks", x => x.id));

        migrationBuilder.CreateIndex(
            name: "ix_stocks_product_id",
            table: "stocks",
            column: "product_id",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "stocks");
    }
}
