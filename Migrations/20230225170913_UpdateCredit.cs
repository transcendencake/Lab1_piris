using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1piris.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCredit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LengthInMonths",
                table: "Credits",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LengthInMonths",
                table: "Credits");
        }
    }
}
