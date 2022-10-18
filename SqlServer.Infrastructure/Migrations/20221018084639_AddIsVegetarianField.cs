using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlServer.Infrastructure.Migrations
{
    public partial class AddIsVegetarianField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVegetarian",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVegetarian",
                table: "Users");
        }
    }
}
