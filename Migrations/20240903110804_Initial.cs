using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HW3._2.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DeadlineTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.CheckConstraint("CK_Task_Deadline", "[DeadlineTime] >= [CreationTime]");
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "DeadlineTime", "Description", "Name", "Status" },
                values: new object[,]
                {
                    { -3, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Make my favorite coffee ", "Make Coffee", 0 },
                    { -2, new DateTime(2024, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Create new Table Task", "Create Table", 0 },
                    { -1, new DateTime(2024, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "TestDescription", "TestTask", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreationTime",
                table: "Tasks",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Name",
                table: "Tasks",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
