using Microsoft.EntityFrameworkCore.Migrations;

namespace AbsenCoordinatWeb.Migrations
{
    public partial class addStatusAbsen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Absens",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Absens");
        }
    }
}
