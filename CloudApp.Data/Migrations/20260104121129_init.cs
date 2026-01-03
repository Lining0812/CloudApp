using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Artist = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Concerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Concerts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_MediaResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContentTpye = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MediaType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MediaResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Artist = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Composer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lyricist = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlbumId = table.Column<int>(type: "int", nullable: true),
                    ConcertId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Tracks_T_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "T_Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_T_Tracks_T_Concerts_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "T_Concerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "T_MediaRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    MediaId = table.Column<int>(type: "int", nullable: false),
                    MediaType = table.Column<int>(type: "int", nullable: false),
                    MediaResourceId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MediaRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_MediaRelations_T_MediaResources_MediaResourceId",
                        column: x => x.MediaResourceId,
                        principalTable: "T_MediaResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Albums_Title",
                table: "T_Albums",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_T_MediaRelations_MediaResourceId",
                table: "T_MediaRelations",
                column: "MediaResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Tracks_AlbumId",
                table: "T_Tracks",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Tracks_ConcertId",
                table: "T_Tracks",
                column: "ConcertId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Tracks_Title",
                table: "T_Tracks",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_MediaRelations");

            migrationBuilder.DropTable(
                name: "T_Tracks");

            migrationBuilder.DropTable(
                name: "T_MediaResources");

            migrationBuilder.DropTable(
                name: "T_Albums");

            migrationBuilder.DropTable(
                name: "T_Concerts");
        }
    }
}
