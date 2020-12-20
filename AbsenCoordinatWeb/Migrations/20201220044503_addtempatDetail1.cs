using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace AbsenCoordinatWeb.Migrations
{
    public partial class addtempatDetail1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempatDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TempatId = table.Column<int>(nullable: false),
                    KaryawanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempatDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempatDetails_Karyawans_KaryawanId",
                        column: x => x.KaryawanId,
                        principalTable: "Karyawans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempatDetails_Tempats_TempatId",
                        column: x => x.TempatId,
                        principalTable: "Tempats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TempatDetails_KaryawanId",
                table: "TempatDetails",
                column: "KaryawanId");

            migrationBuilder.CreateIndex(
                name: "IX_TempatDetails_TempatId",
                table: "TempatDetails",
                column: "TempatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempatDetails");
        }
    }
}
