using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlServer.Infrastructure.Migrations
{
    public partial class AddImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageFormat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_ImageId",
                table: "Games",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Images_ImageId",
                table: "Games",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Images_ImageId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Games_ImageId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

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
                column: "Image",
                value: "https://www.bruna.nl/images/active/carrousel/fullsize/5010993414338_front.jpg");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4,
                column: "Image",
                value: "https://play-lh.googleusercontent.com/vLezygtbLfIe6fi23WCg9Mc4jZn2CW1_6EWBraSCukUGsIpPaBQ7yUN14x4SVggzh3g");
        }
    }
}
