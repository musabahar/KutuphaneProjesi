using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kutuphane.Data.Migrations
{
    public partial class DbOlustur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblKitaplar",
                columns: table => new
                {
                    KitapID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    ISBN = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    YayinYili = table.Column<int>(type: "INT", nullable: false),
                    KapakFiyati = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    Durum = table.Column<byte>(type: "TINYINT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKitaplar", x => x.KitapID);
                });

            migrationBuilder.CreateTable(
                name: "tblKitapHareketler",
                columns: table => new
                {
                    KitapHareketID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KitapID_FK = table.Column<int>(type: "int", nullable: true),
                    Ad = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Soyad = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    TCNo = table.Column<long>(type: "BIGINT", nullable: true),
                    EPosta = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Telefon = table.Column<string>(type: "VARCHAR(12)", nullable: true),
                    BaslangicTarihi = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    BitisTarihi = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    TeslimTarihi = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    GunlukCezaTutari = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false, defaultValue: 5m),
                    ToplamCezaTutari = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    Durum = table.Column<byte>(type: "TINYINT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblKitapHareketler", x => x.KitapHareketID);
                    table.ForeignKey(
                        name: "FK_tblKitapHareketler_KitapID_FK",
                        column: x => x.KitapID_FK,
                        principalTable: "tblKitaplar",
                        principalColumn: "KitapID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblKitapHareketler_KitapID_FK",
                table: "tblKitapHareketler",
                column: "KitapID_FK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblKitapHareketler");

            migrationBuilder.DropTable(
                name: "tblKitaplar");
        }
    }
}
