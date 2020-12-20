using Microsoft.EntityFrameworkCore.Migrations;

namespace AbsenCoordinatWeb.Migrations
{
    public partial class addKaryawanToAbsen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KaryawanId",
                table: "Absens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Absens_KaryawanId",
                table: "Absens",
                column: "KaryawanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absens_Karyawans_KaryawanId",
                table: "Absens",
                column: "KaryawanId",
                principalTable: "Karyawans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absens_Karyawans_KaryawanId",
                table: "Absens");

            migrationBuilder.DropIndex(
                name: "IX_Absens_KaryawanId",
                table: "Absens");

            migrationBuilder.DropColumn(
                name: "KaryawanId",
                table: "Absens");
        }
    }
}
