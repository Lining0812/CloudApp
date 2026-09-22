using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudApp.Infrastructure.Migrations.mysql
{
    /// <inheritdoc />
    public partial class addtracklinkurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkUrl",
                table: "T_Tracks",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkUrl",
                table: "T_Tracks");
        }
    }
}
