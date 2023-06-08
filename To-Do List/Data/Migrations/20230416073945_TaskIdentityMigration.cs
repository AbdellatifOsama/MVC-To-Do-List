using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_Do_List.Data.Migrations
{
    /// <inheritdoc />
    public partial class TaskIdentityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserID",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ApplicationUserID",
                table: "Tasks",
                column: "ApplicationUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_ApplicationUserID",
                table: "Tasks",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_ApplicationUserID",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ApplicationUserID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ApplicationUserID",
                table: "Tasks");
        }
    }
}
