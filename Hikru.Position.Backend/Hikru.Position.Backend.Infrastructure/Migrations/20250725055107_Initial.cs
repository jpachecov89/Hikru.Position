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
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("a1a11111-aaaa-4111-aaaa-1111a1a1a1a1"), "Responsible for software development, architecture and technical innovation.", "Engineering" },
                    { new Guid("a9a99999-a9a9-4999-a9a9-9999a9a9a9a9"), "Supports clients post-sale and ensures their satisfaction and retention.", "Customer Success" },
                    { new Guid("b2b22222-bbbb-4222-bbbb-2222b2b2b2b2"), "Handles branding, digital campaigns, and market research.", "Marketing" },
                    { new Guid("c3c33333-cccc-4333-cccc-3333c3c3c3c3"), "Manages client relationships and drives revenue growth.", "Sales" },
                    { new Guid("d4d44444-dddd-4444-dddd-4444d4d4d4d4"), "Oversees recruitment, employee well-being and internal policies.", "Human Resources" },
                    { new Guid("e5e55555-eeee-4555-eeee-5555e5e5e5e5"), "Controls budgeting, accounting, and financial reporting.", "Finance" },
                    { new Guid("f6f66666-ffff-4666-ffff-6666f6f6f6f6"), "Leads product vision, roadmap and customer experience.", "Product" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Budget", "ClosingDate", "DepartmentId", "Description", "Location", "RecruiterId", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("10101010-aaaa-4aaa-aaaa-aaaaaaaaaaaa"), 110000.0, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1a11111-aaaa-4111-aaaa-1111a1a1a1a1"), "Responsable del desarrollo de servicios backend usando .NET 8 y Azure Functions.", "Lima, Perú", new Guid("a1e0a0f3-11a2-4eaa-8bfb-1b4b1f98a111"), 1, "Senior .NET Developer" },
                    { new Guid("20202020-bbbb-4bbb-bbbb-bbbbbbbbbbbb"), 85000.0, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6f66666-ffff-4666-ffff-6666f6f6f6f6"), "Diseño de interfaces centradas en el usuario para la nueva plataforma SaaS.", "Remoto - LatAm", new Guid("b2c1b1e4-22b3-4fbb-9cfc-2c5c2f09b222"), 1, "UX Designer" },
                    { new Guid("30303030-cccc-4ccc-cccc-cccccccccccc"), 65000.0, null, new Guid("b2b22222-bbbb-4222-bbbb-2222b2b2b2b2"), "Optimización de campañas en Google Ads, SEO y analítica web.", "Santiago, Chile", new Guid("c3d2c2f5-33c4-4bcc-adfd-3d6d3a10c333"), 0, "Digital Marketing Analyst" },
                    { new Guid("40404040-dddd-4ddd-dddd-dddddddddddd"), 48000.0, new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1a11111-aaaa-4111-aaaa-1111a1a1a1a1"), "Desarrollo de interfaces en React y TypeScript. Ideal para primeros trabajos.", "Buenos Aires, Argentina", new Guid("b7f6f6b9-77f8-4bff-bfef-7f0f7a54b777"), 1, "Junior Frontend Developer" },
                    { new Guid("50505050-eeee-4eee-eeee-eeeeeeeeeeee"), 72000.0, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e5e55555-eeee-4555-eeee-5555e5e5e5e5"), "Gestión contable, conciliaciones y reportes mensuales.", "Lima, Perú", new Guid("f6f5f5a8-66f7-4aff-dfef-6f9f6a43f666"), 2, "Accounting Specialist" },
                    { new Guid("60606060-ffff-4fff-ffff-ffffffffffff"), 90000.0, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d4d44444-dddd-4444-dddd-4444d4d4d4d4"), "Gestión del talento, desarrollo organizacional y clima laboral.", "Quito, Ecuador", new Guid("d4e3d3b6-44d5-4bdd-bece-4e7e4a21d444"), 3, "HR Business Partner" },
                    { new Guid("70707070-aaaa-4aaa-bbbb-bbbbbbaaaaaa"), 100000.0, null, new Guid("c3c33333-cccc-4333-cccc-3333c3c3c3c3"), "Venta de software corporativo a clientes grandes en LatAm.", "Remoto - Colombia", new Guid("e5f4e4a7-55e6-4dee-cafe-5f8f5a32e555"), 1, "Sales Executive B2B" }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Recruiters");
        }
    }
}
