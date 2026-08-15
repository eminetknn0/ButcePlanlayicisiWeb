using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ButcePlanlayicisiWeb.Migrations
{
    /// <inheritdoc />
    public partial class IlkVeritabani : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ButceKayitlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Gelir = table.Column<double>(type: "REAL", nullable: false),
                    Gider = table.Column<double>(type: "REAL", nullable: false),
                    Kategori = table.Column<string>(type: "TEXT", nullable: false),
                    Tarih = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ButceKayitlari", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ButceKayitlari");
        }
    }
}
