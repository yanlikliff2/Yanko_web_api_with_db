using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yanko_web3_v2.Migrations
{
    /// <inheritdoc />
    public partial class Migre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_table_Role",
                table: "User_table");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_User_table_role_id",
                table: "User_table");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "User_table");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "User_table",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "User_table");

            migrationBuilder.AddColumn<int>(
                name: "role_id",
                table: "User_table",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.role_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_table_role_id",
                table: "User_table",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_table_Role",
                table: "User_table",
                column: "role_id",
                principalTable: "Role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
