using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlServer.Infrastructure.Migrations
{
    public partial class GameIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "GameNightId", "Genre", "Image", "IsOnlyForAdults", "Name", "Type" },
                values: new object[] { 1, "Versimpelde versie van pesten!", null, 0, "", false, "Uno", 0 });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "GameNightId", "Genre", "Image", "IsOnlyForAdults", "Name", "Type" },
                values: new object[] { 2, "Het spel met treinen!", null, 0, "", false, "Ticket to Ride", 1 });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "GameNightId", "Genre", "Image", "IsOnlyForAdults", "Name", "Type" },
                values: new object[] { 3, "Het spel met geld.", null, 0, "", false, "Monopoly", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "GameNightId", "Genre", "Image", "IsOnlyForAdults", "Name", "Type" },
                values: new object[] { 1, "Versimpelde versie van pesten!", null, 0, "", false, "Uno", 0 });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "GameNightId", "Genre", "Image", "IsOnlyForAdults", "Name", "Type" },
                values: new object[] { 2, "Het spel met treinen!", null, 0, "", false, "Ticket to Ride", 1 });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "GameNightId", "Genre", "Image", "IsOnlyForAdults", "Name", "Type" },
                values: new object[] { 3, "Het spel met geld.", null, 0, "", false, "Monopoly", 1 });
        }
    }
}
