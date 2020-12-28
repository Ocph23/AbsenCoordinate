using Microsoft.EntityFrameworkCore.Migrations;

namespace AbsenCoordinatWeb.Migrations
{
    public partial class addKeteranganAbsen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Keterangan",
                table: "Absens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Keterangan",
                table: "Absens");
        }
    }
}
