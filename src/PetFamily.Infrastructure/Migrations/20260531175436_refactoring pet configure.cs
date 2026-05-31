using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refactoringpetconfigure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "pet_details",
                table: "Pets",
                newName: "requisites_container");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Pets",
                newName: "description_value");

            migrationBuilder.AddColumn<bool>(
                name: "is_published",
                table: "Pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "photos_container",
                table: "Pets",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_published",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "photos_container",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "requisites_container",
                table: "Pets",
                newName: "pet_details");

            migrationBuilder.RenameColumn(
                name: "description_value",
                table: "Pets",
                newName: "description");
        }
    }
}
