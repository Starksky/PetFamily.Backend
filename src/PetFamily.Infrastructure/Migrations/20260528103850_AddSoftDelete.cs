using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "Pets");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "Volunteers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "Pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "Pets",
                column: "volunteer_id",
                principalTable: "Volunteers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "Pets");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "Pets",
                column: "volunteer_id",
                principalTable: "Volunteers",
                principalColumn: "id");
        }
    }
}
