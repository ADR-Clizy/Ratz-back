using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ratz_API.Migrations
{
    public partial class QRCodeupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EncodedUrl",
                table: "QrCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InformationFormat",
                table: "QrCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncodedUrl",
                table: "QrCodes");

            migrationBuilder.DropColumn(
                name: "InformationFormat",
                table: "QrCodes");
        }
    }
}
