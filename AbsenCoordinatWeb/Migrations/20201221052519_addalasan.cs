using Microsoft.EntityFrameworkCore.Migrations;

namespace AbsenCoordinatWeb.Migrations
{
    public partial class addalasan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlasanCuti",
                table: "Cutis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CatatanPersetujuan",
                table: "Cutis",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlasanCuti",
                table: "Cutis");

            migrationBuilder.DropColumn(
                name: "CatatanPersetujuan",
                table: "Cutis");
        }
    }
}
