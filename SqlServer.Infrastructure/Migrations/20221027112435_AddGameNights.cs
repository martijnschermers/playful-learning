using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlServer.Infrastructure.Migrations
{
    public partial class AddGameNights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameNights_GameNightId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_GameNightId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameNightId",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GameGameNight",
                columns: table => new
                {
                    GameNightsId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGameNight", x => new { x.GameNightsId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_GameGameNight_GameNights_GameNightsId",
                        column: x => x.GameNightsId,
                        principalTable: "GameNights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGameNight_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameGameNight_GamesId",
                table: "GameGameNight",
                column: "GamesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGameNight");

            migrationBuilder.AddColumn<int>(
                name: "GameNightId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameNightId",
                table: "Games",
                column: "GameNightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameNights_GameNightId",
                table: "Games",
                column: "GameNightId",
                principalTable: "GameNights",
                principalColumn: "Id");
        }
    }
}
