using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlServer.Infrastructure.Migrations
{
    public partial class RemoveIsVegetarianField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVegetarian",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Allergies",
                columns: new[] { "Id", "Description", "FoodId", "Name", "UserId" },
                values: new object[] { 6, "Vegetariër", null, 5, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Allergies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AddColumn<bool>(
                name: "IsVegetarian",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
