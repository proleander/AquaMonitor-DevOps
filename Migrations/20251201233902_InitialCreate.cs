using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaMonitor.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumosAgua",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Local = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    LitrosConsumidos = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    NivelAlerta = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumosAgua", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumosAgua");
        }
    }
}
