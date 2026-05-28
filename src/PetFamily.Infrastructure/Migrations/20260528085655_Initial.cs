using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_species", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description_value = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    email_value = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    fio_first_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    fio_last_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    fio_patronymic = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    job_age_value = table.Column<int>(type: "integer", nullable: true),
                    phone_value = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    volunteer_details = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Breeds",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    species_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_breeds", x => x.id);
                    table.ForeignKey(
                        name: "fk_breeds_species_species_id",
                        column: x => x.species_id,
                        principalTable: "Species",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    breed_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    color = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    contact_phone = table.Column<string>(type: "text", nullable: true),
                    height = table.Column<double>(type: "double precision", nullable: true),
                    weight = table.Column<double>(type: "double precision", nullable: true),
                    is_vaccinated = table.Column<bool>(type: "boolean", nullable: true),
                    is_neutered = table.Column<bool>(type: "boolean", nullable: true),
                    health_status = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    help_status = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    address_apartment_number = table.Column<int>(type: "integer", nullable: true),
                    address_building_number = table.Column<int>(type: "integer", nullable: true),
                    address_building_number_two = table.Column<int>(type: "integer", nullable: true),
                    address_city = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    address_postal_code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    address_street = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    pet_details = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pets", x => x.id);
                    table.ForeignKey(
                        name: "fk_pets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "Volunteers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_breeds_species_id",
                table: "Breeds",
                column: "species_id");

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                table: "Pets",
                column: "volunteer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Breeds");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropTable(
                name: "Volunteers");
        }
    }
}
