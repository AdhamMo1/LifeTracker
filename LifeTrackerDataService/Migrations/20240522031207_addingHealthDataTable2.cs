using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeTrackerDataService.Migrations
{
    /// <inheritdoc />
    public partial class addingHealthDataTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "HealthData",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_HealthData_ApplicationUserId",
                table: "HealthData",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthData_AspNetUsers_ApplicationUserId",
                table: "HealthData",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthData_AspNetUsers_ApplicationUserId",
                table: "HealthData");

            migrationBuilder.DropIndex(
                name: "IX_HealthData_ApplicationUserId",
                table: "HealthData");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "HealthData");
        }
    }
}
