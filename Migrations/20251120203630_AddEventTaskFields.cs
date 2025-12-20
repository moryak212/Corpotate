using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Corporate.Migrations
{
    /// <inheritdoc />
    public partial class AddEventTaskFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventTasks_Employees_ResponsibleEmployeeId",
                table: "EventTasks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "EventTasks");

            migrationBuilder.RenameColumn(
                name: "ResponsibleEmployeeId",
                table: "EventTasks",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "EventTasks",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_EventTasks_ResponsibleEmployeeId",
                table: "EventTasks",
                newName: "IX_EventTasks_EmployeeId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "EventTasks",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_EventTasks_Employees_EmployeeId",
                table: "EventTasks",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventTasks_Employees_EmployeeId",
                table: "EventTasks");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "EventTasks");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "EventTasks",
                newName: "ResponsibleEmployeeId");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "EventTasks",
                newName: "DueDate");

            migrationBuilder.RenameIndex(
                name: "IX_EventTasks_EmployeeId",
                table: "EventTasks",
                newName: "IX_EventTasks_ResponsibleEmployeeId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "EventTasks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTasks_Employees_ResponsibleEmployeeId",
                table: "EventTasks",
                column: "ResponsibleEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
