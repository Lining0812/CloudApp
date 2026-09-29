using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudApp.Infrastructure.Migrations.mysql
{
    /// <inheritdoc />
    public partial class perfectConcert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartAt",
                table: "T_Concerts",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndAt",
                table: "T_Concerts",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "T_Concerts",
                newName: "Location");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "T_Concerts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "T_Concerts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "T_Concerts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "T_Concerts");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "T_Concerts",
                newName: "StartAt");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "T_Concerts",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "T_Concerts",
                newName: "EndAt");
        }
    }
}
