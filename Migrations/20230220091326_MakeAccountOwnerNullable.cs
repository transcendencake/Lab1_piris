using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1piris.Migrations
{
    /// <inheritdoc />
    public partial class MakeAccountOwnerNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Clients_OwnerId",
                table: "Accounts");

            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "Accounts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Clients_OwnerId",
                table: "Accounts",
                column: "OwnerId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Clients_OwnerId",
                table: "Accounts");

            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "Accounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Clients_OwnerId",
                table: "Accounts",
                column: "OwnerId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
