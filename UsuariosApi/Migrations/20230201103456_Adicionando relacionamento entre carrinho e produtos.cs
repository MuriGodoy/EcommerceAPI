using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosApi.Migrations
{
    public partial class Adicionandorelacionamentoentrecarrinhoeprodutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99996,
                column: "ConcurrencyStamp",
                value: "2bfeac5f-2eef-4618-b381-a4022cd61041");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "fd294b23-95fc-4f61-b1b1-8fed5fc0fb0e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "2d418bc5-b6e3-469e-a292-4c223da0f3b4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "ConcurrencyStamp", "DataCadastro", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8b02fe5e-e73c-482b-a010-e6771186699a", new DateTime(2023, 2, 1, 7, 34, 55, 767, DateTimeKind.Local).AddTicks(7306), "AQAAAAEAACcQAAAAEDPGCS184ZlWXhDCRsKF9/6Af52W+NEp1ZvD4GCOLsmWzY7bxpZ/KAhHwRkgUgEotg==", "eea90984-43b5-4908-abc9-5fa804b295f4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99996,
                column: "ConcurrencyStamp",
                value: "a68c2733-6a34-46be-8a68-e362b2cd7198");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "5dbdd7c9-a424-4425-9658-d4ed819fa1d3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "15baae09-df7e-4c38-b993-8c16eefe8d29");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "ConcurrencyStamp", "DataCadastro", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff2fd292-ad21-4eb2-bbf0-5d9bce9d949d", new DateTime(2022, 12, 27, 7, 12, 55, 129, DateTimeKind.Local).AddTicks(75), "AQAAAAEAACcQAAAAEFpxXU2tzO+lwcjxBdJXfYWTx3iQxQRNtIVZ7qNTJrxreGiecle+eut960SpzncCOQ==", "ff183b37-5954-43c3-a9c5-30f864d92137" });
        }
    }
}
