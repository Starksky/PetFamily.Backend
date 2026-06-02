using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class testfixlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "requisites_container",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "photos_container",
                table: "Pets",
                newName: "requisites");

            migrationBuilder.AddColumn<string>(
                name: "photos",
                table: "Pets",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photos",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "requisites",
                table: "Pets",
                newName: "photos_container");

            migrationBuilder.AddColumn<string>(
                name: "requisites_container",
                table: "Pets",
                type: "jsonb",
                nullable: true);
        }
    }
}
