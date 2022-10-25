using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlServer.Infrastructure.Migrations
{
    public partial class AddSeedGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "https://img.poki.com/cdn-cgi/image/quality=78,width=600,height=600,fit=cover,f=auto/26c6e4e18eeaa62590fccd44ea7812f8.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "https://media.s-bol.com/4zrr6XMkgVXn/550x536.jpg");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Image", "IsOnlyForAdults" },
                values: new object[] { "Het spel met geld!", "https://www.bruna.nl/images/active/carrousel/fullsize/5010993414338_front.jpg", true });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "GameNightId", "Genre", "Image", "IsOnlyForAdults", "Name", "Type" },
                values: new object[] { 4, "Beantwoord zoveel mogelijk vragen in 30 seconde!", null, 0, "https://play-lh.googleusercontent.com/vLezygtbLfIe6fi23WCg9Mc4jZn2CW1_6EWBraSCukUGsIpPaBQ7yUN14x4SVggzh3g", true, "30 Seconds", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Image", "IsOnlyForAdults" },
                values: new object[] { "Het spel met geld.", "", false });
        }
    }
}
