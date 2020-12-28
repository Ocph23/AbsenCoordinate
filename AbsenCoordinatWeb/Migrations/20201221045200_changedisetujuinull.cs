using Microsoft.EntityFrameworkCore.Migrations;

namespace AbsenCoordinatWeb.Migrations
{
    public partial class changedisetujuinull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cutis_Karyawans_DisetuiOlehId",
                table: "Cutis");

            migrationBuilder.AlterColumn<int>(
                name: "DisetuiOlehId",
                table: "Cutis",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cutis_Karyawans_DisetuiOlehId",
                table: "Cutis",
                column: "DisetuiOlehId",
                principalTable: "Karyawans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cutis_Karyawans_DisetuiOlehId",
                table: "Cutis");

            migrationBuilder.AlterColumn<int>(
                name: "DisetuiOlehId",
                table: "Cutis",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cutis_Karyawans_DisetuiOlehId",
                table: "Cutis",
                column: "DisetuiOlehId",
                principalTable: "Karyawans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
