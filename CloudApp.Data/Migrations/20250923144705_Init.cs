using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Albums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Artist = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Artist = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Composer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lyricist = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AlbumId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Tracks_T_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "T_Albums",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Tracks_AlbumId",
                table: "T_Tracks",
                column: "AlbumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Tracks");

            migrationBuilder.DropTable(
                name: "T_Albums");
        }
    }
}
