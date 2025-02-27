using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProektAleks.Migrations
{
    /// <inheritdoc />
    public partial class ProstorBG : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Apartments");
        }
    }
}
