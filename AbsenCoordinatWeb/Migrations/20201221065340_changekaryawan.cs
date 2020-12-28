using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AbsenCoordinatWeb.Migrations
{
    public partial class changekaryawan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LokasiKerja",
                table: "Karyawans",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "NIK",
                table: "Karyawans",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "UnitKerja",
                table: "Karyawans",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TanggalPengajuan",
                table: "Cutis",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Mulai",
                table: "Cutis",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Berakhir",
                table: "Cutis",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AlasanCuti",
                table: "Cutis",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LokasiKerja",
                table: "Karyawans");

            migrationBuilder.DropColumn(
                name: "NIK",
                table: "Karyawans");

            migrationBuilder.DropColumn(
                name: "UnitKerja",
                table: "Karyawans");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TanggalPengajuan",
                table: "Cutis",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Mulai",
                table: "Cutis",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Berakhir",
                table: "Cutis",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "AlasanCuti",
                table: "Cutis",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
