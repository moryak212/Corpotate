using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Corporate.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StoredFileName",
                table: "EventFiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoredFileName",
                table: "EventFiles");
        }
    }
}
