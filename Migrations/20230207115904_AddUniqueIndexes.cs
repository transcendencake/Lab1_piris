using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1piris.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PassportSeries",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PassportId",
                table: "Clients",
                column: "PassportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PassportSeries_PassportNumber",
                table: "Clients",
                columns: new[] { "PassportSeries", "PassportNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_PassportId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_PassportSeries_PassportNumber",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "PassportSeries",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
