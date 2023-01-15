using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rainbowhut1._0.Migrations
{
    /// <inheritdoc />
    public partial class Rainbowhut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Rainbowhut");

            migrationBuilder.CreateTable(
                name: "PROD_GALLERY_DATA",
                schema: "Rainbowhut",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GalleryType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROD_GALLERY_DATA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PROD_PROFILE_DATA",
                schema: "Rainbowhut",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROD_PROFILE_DATA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PROD_QRCODE_DATA",
                schema: "Rainbowhut",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROD_QRCODE_DATA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PROD_SLIDESHOW_DATA",
                schema: "Rainbowhut",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROD_SLIDESHOW_DATA", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PROD_GALLERY_DATA_ID",
                schema: "Rainbowhut",
                table: "PROD_GALLERY_DATA",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROD_PROFILE_DATA_ID",
                schema: "Rainbowhut",
                table: "PROD_PROFILE_DATA",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROD_QRCODE_DATA_ID",
                schema: "Rainbowhut",
                table: "PROD_QRCODE_DATA",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PROD_SLIDESHOW_DATA_ID",
                schema: "Rainbowhut",
                table: "PROD_SLIDESHOW_DATA",
                column: "ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PROD_GALLERY_DATA",
                schema: "Rainbowhut");

            migrationBuilder.DropTable(
                name: "PROD_PROFILE_DATA",
                schema: "Rainbowhut");

            migrationBuilder.DropTable(
                name: "PROD_QRCODE_DATA",
                schema: "Rainbowhut");

            migrationBuilder.DropTable(
                name: "PROD_SLIDESHOW_DATA",
                schema: "Rainbowhut");
        }
    }
}
