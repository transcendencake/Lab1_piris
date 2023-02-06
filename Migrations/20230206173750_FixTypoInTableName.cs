using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1piris.Migrations
{
    /// <inheritdoc />
    public partial class FixTypoInTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Сitizenship_СitizenshipId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "Сitizenship");

            migrationBuilder.DropIndex(
                name: "IX_Clients_СitizenshipId",
                table: "Clients");

            migrationBuilder.AddColumn<long>(
                name: "CitizenshipId",
                table: "Clients",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Citizenship",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizenship", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CitizenshipId",
                table: "Clients",
                column: "CitizenshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Citizenship_CitizenshipId",
                table: "Clients",
                column: "CitizenshipId",
                principalTable: "Citizenship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Citizenship_CitizenshipId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "Citizenship");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CitizenshipId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CitizenshipId",
                table: "Clients");

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

            migrationBuilder.CreateIndex(
                name: "IX_Clients_СitizenshipId",
                table: "Clients",
                column: "СitizenshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Сitizenship_СitizenshipId",
                table: "Clients",
                column: "СitizenshipId",
                principalTable: "Сitizenship",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
