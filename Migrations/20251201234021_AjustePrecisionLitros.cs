using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaMonitor.Api.Migrations
{
    /// <inheritdoc />
    public partial class AjustePrecisionLitros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "LitrosConsumidos",
                table: "ConsumosAgua",
                type: "DECIMAL(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "LitrosConsumidos",
                table: "ConsumosAgua",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,2)",
                oldPrecision: 10,
                oldScale: 2);
        }
    }
}
