using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Complaint_2._0.Migrations
{
    /// <inheritdoc />
    public partial class FixUserComplaintRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Users_UserId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Users_UserId1",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_UserId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_UserId1",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Complaints");

            migrationBuilder.UpdateData(
                table: "ComplaintDocuments",
                keyColumn: "Id",
                keyValue: 1,
                column: "UploadedAt",
                value: new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ComplaintDocuments",
                keyColumn: "Id",
                keyValue: 2,
                column: "UploadedAt",
                value: new DateTime(2024, 1, 15, 10, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Complaints",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Complaints",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 15, 10, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$K5Zy8Zv8Zv8Zv8Zv8Zv8ZuJ8Zv8Zv8Zv8Zv8Zv8Zv8Zv8Zv8Zv8Z");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "PasswordHash" },
                values: new object[] { "agent@complaint.com", "$2a$11$PUnmlFw0gnmDwgcJK6fjse1NFI.k7Eve8FHuKYFa8mb02KyE0/9yG" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "Photo", "Role" },
                values: new object[] { 3, "admin@complaint.com", "Admin User", "$2a$11$dAttqu/pCGlCvarGnFKQoO.xNE2wDPSFfuqedGpM28ZRge1ljcZRK", null, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Complaints",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Complaints",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ComplaintDocuments",
                keyColumn: "Id",
                keyValue: 1,
                column: "UploadedAt",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ComplaintDocuments",
                keyColumn: "Id",
                keyValue: 2,
                column: "UploadedAt",
                value: new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Complaints",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt", "UserId", "UserId1" },
                values: new object[] { new DateTime(2025, 9, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "Complaints",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt", "UserId", "UserId1" },
                values: new object[] { new DateTime(2025, 9, 24, 10, 5, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 24, 10, 5, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEFakeHash1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "PasswordHash" },
                values: new object[] { "agent.smith@example.com", "AQAAAAEAACcQAAAAEFakeHash2" });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_UserId",
                table: "Complaints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_UserId1",
                table: "Complaints",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Users_UserId",
                table: "Complaints",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Users_UserId1",
                table: "Complaints",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
