using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace posss.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produkty",
                columns: table => new
                {
                    ProduktId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: true),
                    KodKreskowy = table.Column<string>(type: "TEXT", nullable: true),
                    Cena = table.Column<double>(type: "REAL", nullable: false),
                    Mag = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkty", x => x.ProduktId);
                });

            migrationBuilder.CreateTable(
                name: "Transakcje",
                columns: table => new
                {
                    TransakcjaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ilosc = table.Column<string>(type: "TEXT", nullable: true),
                    typ = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcje", x => x.TransakcjaId);
                });

            migrationBuilder.CreateTable(
                name: "ProduktTransakcja",
                columns: table => new
                {
                    produktsProduktId = table.Column<int>(type: "INTEGER", nullable: false),
                    transaksTransakcjaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduktTransakcja", x => new { x.produktsProduktId, x.transaksTransakcjaId });
                    table.ForeignKey(
                        name: "FK_ProduktTransakcja_Produkty_produktsProduktId",
                        column: x => x.produktsProduktId,
                        principalTable: "Produkty",
                        principalColumn: "ProduktId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduktTransakcja_Transakcje_transaksTransakcjaId",
                        column: x => x.transaksTransakcjaId,
                        principalTable: "Transakcje",
                        principalColumn: "TransakcjaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProduktTransakcja_transaksTransakcjaId",
                table: "ProduktTransakcja",
                column: "transaksTransakcjaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduktTransakcja");

            migrationBuilder.DropTable(
                name: "Produkty");

            migrationBuilder.DropTable(
                name: "Transakcje");
        }
    }
}
