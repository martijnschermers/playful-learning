using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SqlServer.Infrastructure.Migrations
{
    public partial class AddAllergyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergy_Food_FoodId",
                table: "Allergy");

            migrationBuilder.DropForeignKey(
                name: "FK_Allergy_Users_UserId",
                table: "Allergy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Allergy",
                table: "Allergy");

            migrationBuilder.RenameTable(
                name: "Allergy",
                newName: "Allergies");

            migrationBuilder.RenameIndex(
                name: "IX_Allergy_UserId",
                table: "Allergies",
                newName: "IX_Allergies_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Allergy_FoodId",
                table: "Allergies",
                newName: "IX_Allergies_FoodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allergies",
                table: "Allergies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_Food_FoodId",
                table: "Allergies",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergies_Users_UserId",
                table: "Allergies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_Food_FoodId",
                table: "Allergies");

            migrationBuilder.DropForeignKey(
                name: "FK_Allergies_Users_UserId",
                table: "Allergies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Allergies",
                table: "Allergies");

            migrationBuilder.RenameTable(
                name: "Allergies",
                newName: "Allergy");

            migrationBuilder.RenameIndex(
                name: "IX_Allergies_UserId",
                table: "Allergy",
                newName: "IX_Allergy_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Allergies_FoodId",
                table: "Allergy",
                newName: "IX_Allergy_FoodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allergy",
                table: "Allergy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergy_Food_FoodId",
                table: "Allergy",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergy_Users_UserId",
                table: "Allergy",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
