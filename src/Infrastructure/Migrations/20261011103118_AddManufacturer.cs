using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddManufacturer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "NonPaperMedias");

            migrationBuilder.AddColumn<int>(
                name: "ManufacturerId",
                table: "NonPaperMedias",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NonPaperMedias_ManufacturerId",
                table: "NonPaperMedias",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_Name",
                table: "Manufacturers",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NonPaperMedias_Manufacturers_ManufacturerId",
                table: "NonPaperMedias",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NonPaperMedias_Manufacturers_ManufacturerId",
                table: "NonPaperMedias");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "IX_NonPaperMedias_ManufacturerId",
                table: "NonPaperMedias");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "NonPaperMedias");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "NonPaperMedias",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
