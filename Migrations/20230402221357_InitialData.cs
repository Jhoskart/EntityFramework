using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace entityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("4ed10ffb-a8cf-4360-953f-1173f0a1c202"), "Descripción de la categoría 1", "Actividades personales", 50 },
                    { new Guid("4ed10ffb-a8cf-4360-953f-1173f0a1c2e0"), "Descripción de la categoría 1", "Actividades pendientes", 20 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[,]
                {
                    { new Guid("4ed10ffb-a8cf-4360-953f-1173f0a1c210"), new Guid("4ed10ffb-a8cf-4360-953f-1173f0a1c2e0"), null, new DateTime(2023, 4, 2, 18, 13, 57, 499, DateTimeKind.Local).AddTicks(1090), 1, "Revisar pago de servicios publicos" },
                    { new Guid("4ed10ffb-a8cf-4360-953f-1173f0a1c211"), new Guid("4ed10ffb-a8cf-4360-953f-1173f0a1c202"), null, new DateTime(2023, 4, 2, 18, 13, 57, 499, DateTimeKind.Local).AddTicks(1155), 0, "Terminar de ver peliculas de Marvel" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("4ed10ffb-a8cf-4360-953f-1173f0a1c210"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("4ed10ffb-a8cf-4360-953f-1173f0a1c211"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("4ed10ffb-a8cf-4360-953f-1173f0a1c202"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("4ed10ffb-a8cf-4360-953f-1173f0a1c2e0"));
        }
    }
}
