using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlServer.Infrastructure.Migrations
{
    public partial class IsOnlyForAdults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsForAdults",
                table: "Games",
                newName: "IsOnlyForAdults");

            migrationBuilder.AddColumn<bool>(
                name: "IsOnlyForAdults",
                table: "GameNights",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnlyForAdults",
                table: "GameNights");

            migrationBuilder.RenameColumn(
                name: "IsOnlyForAdults",
                table: "Games",
                newName: "IsForAdults");
        }
    }
}
