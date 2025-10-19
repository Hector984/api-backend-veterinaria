using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Veterinaria.Migrations
{
    /// <inheritdoc />
    public partial class AgregarColumnasDeFechasVeterinarias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                schema: "public",
                table: "Veterinarias",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaBaja",
                schema: "public",
                table: "Veterinarias",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                schema: "public",
                table: "Veterinarias",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                schema: "public",
                table: "Veterinarias");

            migrationBuilder.DropColumn(
                name: "FechaBaja",
                schema: "public",
                table: "Veterinarias");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                schema: "public",
                table: "Veterinarias");
        }
    }
}
