using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryWeb.Migrations
{
    /// <inheritdoc />
    public partial class up : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Members_MemberId",
                table: "Loans");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "Loans",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MemberId1",
                table: "Loans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_MemberId1",
                table: "Loans",
                column: "MemberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_AspNetUsers_MemberId",
                table: "Loans",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Members_MemberId1",
                table: "Loans",
                column: "MemberId1",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_AspNetUsers_MemberId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Members_MemberId1",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_MemberId1",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "Loans");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Loans",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Members_MemberId",
                table: "Loans",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
