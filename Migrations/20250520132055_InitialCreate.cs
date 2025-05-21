using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ekzVar.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Oboi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    OsnovaMaterialId = table.Column<int>(type: "INTEGER", nullable: false),
                    PokritieMaterialId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oboi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oboi_Materials_OsnovaMaterialId",
                        column: x => x.OsnovaMaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Oboi_Materials_PokritieMaterialId",
                        column: x => x.PokritieMaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Oboi_OsnovaMaterialId",
                table: "Oboi",
                column: "OsnovaMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Oboi_PokritieMaterialId",
                table: "Oboi",
                column: "PokritieMaterialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oboi");

            migrationBuilder.DropTable(
                name: "Materials");
        }
    }
}
