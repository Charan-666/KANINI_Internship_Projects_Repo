using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Complaint_2._0.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplaintTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Agents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Citizens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizens", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Citizens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComplaintTypeId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CitizenId = table.Column<int>(type: "int", nullable: false),
                    AgentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complaints_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Complaints_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Complaints_ComplaintTypes_ComplaintTypeId",
                        column: x => x.ComplaintTypeId,
                        principalTable: "ComplaintTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Complaints_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Complaints_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComplaintDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplaintDocuments_Complaints_ComplaintId",
                        column: x => x.ComplaintId,
                        principalTable: "Complaints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ComplaintTypes",
                columns: new[] { "Id", "Name", "TypeName" },
                values: new object[,]
                {
                    { 1, "Aadhar", "Aadhar Card Issues" },
                    { 2, "VoterID", "Voter ID Issues" },
                    { 3, "BirthCertificate", "Birth Certificate Issues" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "Photo", "Role" },
                values: new object[,]
                {
                    { 1, "john@example.com", "John Citizen", "AQAAAAEAACcQAAAAEFakeHash1", null, 0 },
                    { 2, "agent.smith@example.com", "Agent Smith", "AQAAAAEAACcQAAAAEFakeHash2", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Agents",
                columns: new[] { "UserId", "Department" },
                values: new object[] { 2, "Aadhar" });

            migrationBuilder.InsertData(
                table: "Citizens",
                columns: new[] { "UserId", "Address", "PhoneNumber" },
                values: new object[] { 1, "123 Main Street", "9999999999" });

            migrationBuilder.InsertData(
                table: "Complaints",
                columns: new[] { "Id", "AgentId", "CitizenId", "ComplaintTypeId", "CreatedAt", "Description", "Status", "Title", "UpdatedAt", "UserId", "UserId1" },
                values: new object[,]
                {
                    { 1, 2, 1, 1, new DateTime(2025, 9, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), "Aadhar not updated", 0, "Aadhar Card Issue", new DateTime(2025, 9, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 2, 2, 1, 2, new DateTime(2025, 9, 24, 10, 5, 0, 0, DateTimeKind.Unspecified), "Name mismatch in Voter ID", 0, "Voter ID Name Mismatch", new DateTime(2025, 9, 24, 10, 5, 0, 0, DateTimeKind.Unspecified), null, null }
                });

            migrationBuilder.InsertData(
                table: "ComplaintDocuments",
                columns: new[] { "Id", "ComplaintId", "ContentType", "Data", "FileName", "Type", "UploadedAt" },
                values: new object[,]
                {
                    { 1, 1, "application/pdf", new byte[0], "aadhar_doc.pdf", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "application/pdf", new byte[0], "voter_doc.pdf", 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintDocuments_ComplaintId",
                table: "ComplaintDocuments",
                column: "ComplaintId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_AgentId",
                table: "Complaints",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_CitizenId",
                table: "Complaints",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ComplaintTypeId",
                table: "Complaints",
                column: "ComplaintTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_UserId",
                table: "Complaints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_UserId1",
                table: "Complaints",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplaintDocuments");

            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Citizens");

            migrationBuilder.DropTable(
                name: "ComplaintTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
