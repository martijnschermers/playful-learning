using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlServer.Infrastructure.Migrations
{
    public partial class AddDrinksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drink_GameNights_GameNightId",
                table: "Drink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drink",
                table: "Drink");

            migrationBuilder.RenameTable(
                name: "Drink",
                newName: "Drinks");

            migrationBuilder.RenameIndex(
                name: "IX_Drink_GameNightId",
                table: "Drinks",
                newName: "IX_Drinks_GameNightId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drinks",
                table: "Drinks",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "ContainsAlcohol", "GameNightId", "Name" },
                values: new object[,]
                {
                    { 1, true, null, "Bier" },
                    { 2, false, null, "Water" },
                    { 3, false, null, "Cola" },
                    { 4, true, null, "Wijn" },
                    { 5, false, null, "Fanta" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_GameNights_GameNightId",
                table: "Drinks",
                column: "GameNightId",
                principalTable: "GameNights",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_GameNights_GameNightId",
                table: "Drinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drinks",
                table: "Drinks");

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameTable(
                name: "Drinks",
                newName: "Drink");

            migrationBuilder.RenameIndex(
                name: "IX_Drinks_GameNightId",
                table: "Drink",
                newName: "IX_Drink_GameNightId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drink",
                table: "Drink",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drink_GameNights_GameNightId",
                table: "Drink",
                column: "GameNightId",
                principalTable: "GameNights",
                principalColumn: "Id");
        }
    }
}
