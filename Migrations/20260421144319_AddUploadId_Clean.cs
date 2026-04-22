using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Visitingcardgenerator.Migrations
{
    /// <inheritdoc />
    public partial class AddUploadIdClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UploadId",
                table: "CustomerInfo",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadId",
                table: "CustomerInfo");
        }
    }
}
