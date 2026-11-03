using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HistoryRebuild1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WorkstationHistoryItems_UserId",
                table: "WorkstationHistoryItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkstationHistoryItems_WorkstationId",
                table: "WorkstationHistoryItems",
                column: "WorkstationId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkstationHistoryItems_Users_UserId",
                table: "WorkstationHistoryItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkstationHistoryItems_Workstations_WorkstationId",
                table: "WorkstationHistoryItems",
                column: "WorkstationId",
                principalTable: "Workstations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkstationHistoryItems_Users_UserId",
                table: "WorkstationHistoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkstationHistoryItems_Workstations_WorkstationId",
                table: "WorkstationHistoryItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkstationHistoryItems_UserId",
                table: "WorkstationHistoryItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkstationHistoryItems_WorkstationId",
                table: "WorkstationHistoryItems");
        }
    }
}
