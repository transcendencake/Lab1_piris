using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1piris.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disabilities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FamilyStates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Сitizenship",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Сitizenship", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsMale = table.Column<bool>(type: "bit", nullable: false),
                    PassportSeries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    PassportIssuedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportIssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassportId = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    BirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LivingCityId = table.Column<long>(type: "bigint", nullable: false),
                    LivingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomePhone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkingPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationCityId = table.Column<long>(type: "bigint", nullable: false),
                    FamilyStateId = table.Column<long>(type: "bigint", nullable: false),
                    СitizenshipId = table.Column<long>(type: "bigint", nullable: false),
                    DisabilityId = table.Column<long>(type: "bigint", nullable: false),
                    Pensioner = table.Column<bool>(type: "bit", nullable: false),
                    MonthIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Cities_LivingCityId",
                        column: x => x.LivingCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_Cities_RegistrationCityId",
                        column: x => x.RegistrationCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_Disabilities_DisabilityId",
                        column: x => x.DisabilityId,
                        principalTable: "Disabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_FamilyStates_FamilyStateId",
                        column: x => x.FamilyStateId,
                        principalTable: "FamilyStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_Сitizenship_СitizenshipId",
                        column: x => x.СitizenshipId,
                        principalTable: "Сitizenship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DisabilityId",
                table: "Clients",
                column: "DisabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_FamilyStateId",
                table: "Clients",
                column: "FamilyStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_LivingCityId",
                table: "Clients",
                column: "LivingCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_RegistrationCityId",
                table: "Clients",
                column: "RegistrationCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_СitizenshipId",
                table: "Clients",
                column: "СitizenshipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Disabilities");

            migrationBuilder.DropTable(
                name: "FamilyStates");

            migrationBuilder.DropTable(
                name: "Сitizenship");
        }
    }
}
