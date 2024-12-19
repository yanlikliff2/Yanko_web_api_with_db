using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yanko_web3_v2.Migrations
{
    /// <inheritdoc />
    public partial class ado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
name: "Password",
table: "User_table",
maxLength: 100,
nullable: true,
oldClrType: typeof(string),
oldType: "nvarchar(20)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
       name: "Password",
       table: "User_table",
       maxLength: 20,
       nullable: true,
       oldClrType: typeof(string),
       oldMaxLength: 100);
        }
    }
}
