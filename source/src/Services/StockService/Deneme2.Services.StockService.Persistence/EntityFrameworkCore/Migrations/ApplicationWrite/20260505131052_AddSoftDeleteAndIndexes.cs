using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deneme2.Services.StockService.Persistence.EntityFrameworkCore.Migrations.ApplicationWrite;

/// <inheritdoc />
public partial class AddSoftDeleteAndIndexes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "pk_stocks",
            table: "stocks");

        migrationBuilder.RenameTable(
            name: "stocks",
            newName: "Stocks");

        migrationBuilder.RenameColumn(
            name: "quantity",
            table: "Stocks",
            newName: "Quantity");

        migrationBuilder.RenameColumn(
            name: "id",
            table: "Stocks",
            newName: "Id");

        migrationBuilder.RenameColumn(
            name: "updated_by",
            table: "Stocks",
            newName: "UpdatedBy");

        migrationBuilder.RenameColumn(
            name: "updated_at",
            table: "Stocks",
            newName: "UpdatedAt");

        migrationBuilder.RenameColumn(
            name: "row_version",
            table: "Stocks",
            newName: "RowVersion");

        migrationBuilder.RenameColumn(
            name: "product_id",
            table: "Stocks",
            newName: "ProductId");

        migrationBuilder.RenameColumn(
            name: "created_by",
            table: "Stocks",
            newName: "CreatedBy");

        migrationBuilder.RenameColumn(
            name: "created_at",
            table: "Stocks",
            newName: "CreatedAt");

        migrationBuilder.RenameIndex(
            name: "ix_stocks_product_id",
            table: "Stocks",
            newName: "IX_Stocks_ProductId");

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "DeletedAt",
            table: "Stocks",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "DeletedBy",
            table: "Stocks",
            type: "character varying(40)",
            maxLength: 40,
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Stocks",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddPrimaryKey(
            name: "PK_Stocks",
            table: "Stocks",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_Stocks_CreatedAt_IsDeleted",
            table: "Stocks",
            columns: ["CreatedAt", "IsDeleted"]);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_Stocks",
            table: "Stocks");

        migrationBuilder.DropIndex(
            name: "IX_Stocks_CreatedAt_IsDeleted",
            table: "Stocks");

        migrationBuilder.DropColumn(
            name: "DeletedAt",
            table: "Stocks");

        migrationBuilder.DropColumn(
            name: "DeletedBy",
            table: "Stocks");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Stocks");

        migrationBuilder.RenameTable(
            name: "Stocks",
            newName: "stocks");

        migrationBuilder.RenameColumn(
            name: "Quantity",
            table: "stocks",
            newName: "quantity");

        migrationBuilder.RenameColumn(
            name: "Id",
            table: "stocks",
            newName: "id");

        migrationBuilder.RenameColumn(
            name: "UpdatedBy",
            table: "stocks",
            newName: "updated_by");

        migrationBuilder.RenameColumn(
            name: "UpdatedAt",
            table: "stocks",
            newName: "updated_at");

        migrationBuilder.RenameColumn(
            name: "RowVersion",
            table: "stocks",
            newName: "row_version");

        migrationBuilder.RenameColumn(
            name: "ProductId",
            table: "stocks",
            newName: "product_id");

        migrationBuilder.RenameColumn(
            name: "CreatedBy",
            table: "stocks",
            newName: "created_by");

        migrationBuilder.RenameColumn(
            name: "CreatedAt",
            table: "stocks",
            newName: "created_at");

        migrationBuilder.RenameIndex(
            name: "IX_Stocks_ProductId",
            table: "stocks",
            newName: "ix_stocks_product_id");

        migrationBuilder.AddPrimaryKey(
            name: "pk_stocks",
            table: "stocks",
            column: "id");
    }
}
