using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hikru.Position.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recruiters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seniority = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruiters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RecruiterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Positions_Recruiters_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "Recruiters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("3d2897ed-1b11-4f99-b26a-92a3baea5a79"), "Controls budgeting, accounting, and financial reporting.", "Finance" },
                    { new Guid("510a002e-510a-490c-8433-0808bc0215b3"), "Supports clients post-sale and ensures their satisfaction and retention.", "Customer Success" },
                    { new Guid("7a79f0c2-f8fd-4a59-ba0c-5096fe7b81fe"), "Responsible for software development, architecture and technical innovation.", "Engineering" },
                    { new Guid("9a3ba9c6-2549-485e-b111-4a0ffc60c19a"), "Handles branding, digital campaigns, and market research.", "Marketing" },
                    { new Guid("9bb98dbd-1b21-494a-a9a5-6b7e3967a5dc"), "Leads product vision, roadmap and customer experience.", "Product" },
                    { new Guid("9f45c02f-dfe2-476d-9ffb-4fda843e8181"), "Manages client relationships and drives revenue growth.", "Sales" },
                    { new Guid("ff722dd0-1fac-4b5f-8d07-77e43d82fe1f"), "Oversees recruitment, employee well-being and internal policies.", "Human Resources" }
                });

            migrationBuilder.InsertData(
                table: "Recruiters",
                columns: new[] { "Id", "Email", "Name", "Phone", "Seniority" },
                values: new object[,]
                {
                    { new Guid("a1e0a0f3-11a2-4eaa-8bfb-1b4b1f98a111"), "laura.fernandez@hikru.com", "Laura Fernández", "+51 987 654 321", "Senior" },
                    { new Guid("b2c1b1e4-22b3-4fbb-9cfc-2c5c2f09b222"), "carlos.rojas@hikru.com", "Carlos Rojas", "+51 984 123 456", "Mid" },
                    { new Guid("b7f6f6b9-77f8-4bff-bfef-7f0f7a54b777"), "maria.delgado@hikru.com", "María Delgado", "+51 983 555 666", "Junior" },
                    { new Guid("c3d2c2f5-33c4-4bcc-adfd-3d6d3a10c333"), "sofia.mendez@hikru.com", "Sofía Méndez", "+51 982 111 222", "Junior" },
                    { new Guid("d4e3d3b6-44d5-4bdd-bece-4e7e4a21d444"), "jorge.castillo@hikru.com", "Jorge Castillo", "+51 986 777 888", "Senior" },
                    { new Guid("e5f4e4a7-55e6-4dee-cafe-5f8f5a32e555"), "camila.ruiz@hikru.com", "Camila Ruiz", "+51 985 222 333", "Mid" },
                    { new Guid("f6f5f5a8-66f7-4aff-dfef-6f9f6a43f666"), "luis.paredes@hikru.com", "Luis Paredes", "+51 981 444 555", "Senior" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Budget", "ClosingDate", "DepartmentId", "Description", "Location", "RecruiterId", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("0a019559-4cbd-4828-bed9-a2879b74378f"), 48000.0, new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7a79f0c2-f8fd-4a59-ba0c-5096fe7b81fe"), "Interface development in React and TypeScript. Ideal for beginners.", "Buenos Aires, Argentina", new Guid("b7f6f6b9-77f8-4bff-bfef-7f0f7a54b777"), 1, "Junior Frontend Developer" },
                    { new Guid("15ef6e40-eb52-4b68-8bf6-b9872fda2db9"), 65000.0, null, new Guid("9a3ba9c6-2549-485e-b111-4a0ffc60c19a"), "Google Ads campaign optimization, SEO, and web analytics.", "Santiago, Chile", new Guid("c3d2c2f5-33c4-4bcc-adfd-3d6d3a10c333"), 0, "Digital Marketing Analyst" },
                    { new Guid("4a508dcd-3ca8-4b4c-a53c-bae0a5c4f917"), 90000.0, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ff722dd0-1fac-4b5f-8d07-77e43d82fe1f"), "Talent management, organizational development and work environment.", "Quito, Ecuador", new Guid("d4e3d3b6-44d5-4bdd-bece-4e7e4a21d444"), 3, "HR Business Partner" },
                    { new Guid("74727422-18ea-4b75-b3c8-389e8fceb14a"), 72000.0, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3d2897ed-1b11-4f99-b26a-92a3baea5a79"), "Accounting management, reconciliations and monthly reports.", "Lima, Perú", new Guid("f6f5f5a8-66f7-4aff-dfef-6f9f6a43f666"), 2, "Accounting Specialist" },
                    { new Guid("932b6617-9fff-456b-8713-73cc5a3312d8"), 100000.0, null, new Guid("9f45c02f-dfe2-476d-9ffb-4fda843e8181"), "Sales of corporate software to large clients in Latin America.", "Remote - LatAm", new Guid("e5f4e4a7-55e6-4dee-cafe-5f8f5a32e555"), 1, "Sales Executive B2B" },
                    { new Guid("c965a79b-e096-43ee-acea-7a2f71450dc9"), 85000.0, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9bb98dbd-1b21-494a-a9a5-6b7e3967a5dc"), "Designing user-centric interfaces for the new SaaS platform.", "Remote - LatAm", new Guid("b2c1b1e4-22b3-4fbb-9cfc-2c5c2f09b222"), 1, "UX Designer" },
                    { new Guid("e1b61dfb-9e82-4bf9-9a68-f86ee8db7031"), 110000.0, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7a79f0c2-f8fd-4a59-ba0c-5096fe7b81fe"), "Responsible for the development of backend services using .NET 8 and Azure Functions.", "Lima, Perú", new Guid("a1e0a0f3-11a2-4eaa-8bfb-1b4b1f98a111"), 1, "Senior .NET Developer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DepartmentId",
                table: "Positions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_RecruiterId",
                table: "Positions",
                column: "RecruiterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Recruiters");
        }
    }
}
