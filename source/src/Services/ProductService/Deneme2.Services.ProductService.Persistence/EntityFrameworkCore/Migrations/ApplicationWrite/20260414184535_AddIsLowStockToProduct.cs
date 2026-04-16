using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deneme2.Services.ProductService.Persistence.EntityFrameworkCore.Migrations.ApplicationWrite;

/// <inheritdoc />
public partial class AddIsLowStockToProduct : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "is_low_stock",
            table: "products",
            type: "boolean",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "is_low_stock",
            table: "products");
    }
}
