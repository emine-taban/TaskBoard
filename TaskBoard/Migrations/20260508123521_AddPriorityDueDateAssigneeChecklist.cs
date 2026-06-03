using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoard.Migrations
{
    /// <inheritdoc />
    public partial class AddPriorityDueDateAssigneeChecklist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "TaskCards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "TaskCards",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "TaskCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChecklistItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskCardId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistItems_TaskCards_TaskCardId",
                        column: x => x.TaskCardId,
                        principalTable: "TaskCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskCards_AssignedUserId",
                table: "TaskCards",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItems_TaskCardId",
                table: "ChecklistItems",
                column: "TaskCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCards_Users_AssignedUserId",
                table: "TaskCards",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskCards_Users_AssignedUserId",
                table: "TaskCards");

            migrationBuilder.DropTable(
                name: "ChecklistItems");

            migrationBuilder.DropIndex(
                name: "IX_TaskCards_AssignedUserId",
                table: "TaskCards");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "TaskCards");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "TaskCards");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "TaskCards");
        }
    }
}
