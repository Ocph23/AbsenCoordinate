using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace AbsenCoordinatWeb.Migrations
{
    public partial class cuti1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cutis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Mulai = table.Column<DateTime>(nullable: true),
                    Berakhir = table.Column<DateTime>(nullable: true),
                    TanggalPengajuan = table.Column<DateTime>(nullable: true),
                    StatusPengajuan = table.Column<int>(nullable: false),
                    KaryawanId = table.Column<int>(nullable: false),
                    DisetuiOlehId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cutis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cutis_Karyawans_DisetuiOlehId",
                        column: x => x.DisetuiOlehId,
                        principalTable: "Karyawans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cutis_Karyawans_KaryawanId",
                        column: x => x.KaryawanId,
                        principalTable: "Karyawans",  
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cutis_DisetuiOlehId",
                table: "Cutis",
                column: "DisetuiOlehId");

            migrationBuilder.CreateIndex(
                name: "IX_Cutis_KaryawanId",
                table: "Cutis",
                column: "KaryawanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cutis");
        }
    }
}
