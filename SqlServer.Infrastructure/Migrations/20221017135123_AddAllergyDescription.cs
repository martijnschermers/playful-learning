using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlServer.Infrastructure.Migrations
{
    public partial class AddAllergyDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Allergy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Allergy",
                columns: new[] { "Id", "Description", "FoodId", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Gluten", null, 4, null },
                    { 2, "Lactose", null, 1, null },
                    { 3, "Noten", null, 0, null },
                    { 4, "Soja", null, 2, null },
                    { 5, "Tarwe", null, 3, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Allergy",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Allergy",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Allergy",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Allergy",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Allergy",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Allergy");
        }
    }
}
