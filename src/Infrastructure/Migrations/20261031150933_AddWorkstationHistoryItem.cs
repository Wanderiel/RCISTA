using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkstationHistoryItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkstationHistoryItemId",
                table: "NonPaperMedias",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkstationStateRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkstationId = table.Column<int>(type: "integer", nullable: false),
                    ActionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    WorkDescription = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    TaskNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkstationStateRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NonPaperMedias_WorkstationHistoryItemId",
                table: "NonPaperMedias",
                column: "WorkstationHistoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_NonPaperMedias_WorkstationStateRecords_WorkstationHistoryIt~",
                table: "NonPaperMedias",
                column: "WorkstationHistoryItemId",
                principalTable: "WorkstationStateRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NonPaperMedias_WorkstationStateRecords_WorkstationHistoryIt~",
                table: "NonPaperMedias");

            migrationBuilder.DropTable(
                name: "WorkstationStateRecords");

            migrationBuilder.DropIndex(
                name: "IX_NonPaperMedias_WorkstationHistoryItemId",
                table: "NonPaperMedias");

            migrationBuilder.DropColumn(
                name: "WorkstationHistoryItemId",
                table: "NonPaperMedias");
        }
    }
}
