using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Corporate.Migrations
{
    /// <inheritdoc />
    public partial class MakeOrganizerOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorporateEvents_Employees_OrganizerId",
                table: "CorporateEvents");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerId",
                table: "CorporateEvents",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_CorporateEvents_Employees_OrganizerId",
                table: "CorporateEvents",
                column: "OrganizerId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorporateEvents_Employees_OrganizerId",
                table: "CorporateEvents");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizerId",
                table: "CorporateEvents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CorporateEvents_Employees_OrganizerId",
                table: "CorporateEvents",
                column: "OrganizerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
