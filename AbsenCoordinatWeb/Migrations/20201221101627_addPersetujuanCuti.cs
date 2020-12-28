using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace AbsenCoordinatWeb.Migrations
{
    public partial class addPersetujuanCuti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cutis_Karyawans_DisetuiOlehId",
                table: "Cutis");

            migrationBuilder.DropIndex(
                name: "IX_Cutis_DisetuiOlehId",
                table: "Cutis");

            migrationBuilder.DropColumn(
                name: "CatatanPersetujuan",
                table: "Cutis");

            migrationBuilder.DropColumn(
                name: "DisetuiOlehId",
                table: "Cutis");

            migrationBuilder.DropColumn(
                name: "StatusPengajuan",
                table: "Cutis");

            migrationBuilder.CreateTable(
                name: "Persetujuan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CutiId = table.Column<int>(nullable: false),
                    Mulai = table.Column<DateTime>(nullable: false),
                    Berakhir = table.Column<DateTime>(nullable: false),
                    TanggalPersetujuan = table.Column<DateTime>(nullable: false),
                    StatusPengajuan = table.Column<int>(nullable: false),
                    CatatanPersetujuan = table.Column<string>(nullable: false),
                    KaryawanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persetujuan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persetujuan_Cutis_CutiId",
                        column: x => x.CutiId,
                        principalTable: "Cutis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persetujuan_Karyawans_KaryawanId",
                        column: x => x.KaryawanId,
                        principalTable: "Karyawans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persetujuan_CutiId",
                table: "Persetujuan",
                column: "CutiId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persetujuan_KaryawanId",
                table: "Persetujuan",
                column: "KaryawanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persetujuan");

            migrationBuilder.AddColumn<string>(
                name: "CatatanPersetujuan",
                table: "Cutis",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisetuiOlehId",
                table: "Cutis",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusPengajuan",
                table: "Cutis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cutis_DisetuiOlehId",
                table: "Cutis",
                column: "DisetuiOlehId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cutis_Karyawans_DisetuiOlehId",
                table: "Cutis",
                column: "DisetuiOlehId",
                principalTable: "Karyawans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
