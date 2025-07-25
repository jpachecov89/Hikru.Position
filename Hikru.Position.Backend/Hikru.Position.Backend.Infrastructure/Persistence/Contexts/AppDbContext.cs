using Hikru.Position.Backend.Domain.Entities;
using Hikru.Position.Backend.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Hikru.Position.Backend.Infrastructure.Persistence.Contexts
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<Recruiter> Recruiters => Set<Recruiter>();
		public DbSet<Department> Departments => Set<Department>();
		public DbSet<Domain.Entities.Position> Positions => Set<Domain.Entities.Position>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Domain.Entities.Position>(entity =>
			{
				entity.Property(x => x.Title).HasMaxLength(100).IsRequired();
				entity.Property(x => x.Description).HasMaxLength(1000).IsRequired();
			});

			modelBuilder.Entity<Recruiter>(entity =>
			{
				entity.HasData(
					new Recruiter
					{
						Id = Guid.Parse("a1e0a0f3-11a2-4eaa-8bfb-1b4b1f98a111"),
						Name = "Laura Fernández",
						Email = "laura.fernandez@hikru.com",
						Phone = "+51 987 654 321",
						Seniority = "Senior"
					},
					new Recruiter
					{
						Id = Guid.Parse("b2c1b1e4-22b3-4fbb-9cfc-2c5c2f09b222"),
						Name = "Carlos Rojas",
						Email = "carlos.rojas@hikru.com",
						Phone = "+51 984 123 456",
						Seniority = "Mid"
					},
					new Recruiter
					{
						Id = Guid.Parse("c3d2c2f5-33c4-4bcc-adfd-3d6d3a10c333"),
						Name = "Sofía Méndez",
						Email = "sofia.mendez@hikru.com",
						Phone = "+51 982 111 222",
						Seniority = "Junior"
					},
					new Recruiter
					{
						Id = Guid.Parse("d4e3d3b6-44d5-4bdd-bece-4e7e4a21d444"),
						Name = "Jorge Castillo",
						Email = "jorge.castillo@hikru.com",
						Phone = "+51 986 777 888",
						Seniority = "Senior"
					},
					new Recruiter
					{
						Id = Guid.Parse("e5f4e4a7-55e6-4dee-cafe-5f8f5a32e555"),
						Name = "Camila Ruiz",
						Email = "camila.ruiz@hikru.com",
						Phone = "+51 985 222 333",
						Seniority = "Mid"
					},
					new Recruiter
					{
						Id = Guid.Parse("f6f5f5a8-66f7-4aff-dfef-6f9f6a43f666"),
						Name = "Luis Paredes",
						Email = "luis.paredes@hikru.com",
						Phone = "+51 981 444 555",
						Seniority = "Senior"
					},
					new Recruiter
					{
						Id = Guid.Parse("b7f6f6b9-77f8-4bff-bfef-7f0f7a54b777"),
						Name = "María Delgado",
						Email = "maria.delgado@hikru.com",
						Phone = "+51 983 555 666",
						Seniority = "Junior"
					}
				);
			});

			modelBuilder.Entity<Department>(entity =>
			{
				entity.HasData(
					new Department
					{
						Id = Guid.Parse("a1a11111-aaaa-4111-aaaa-1111a1a1a1a1"),
						Name = "Engineering",
						Description = "Responsible for software development, architecture and technical innovation."
					},
					new Department
					{
						Id = Guid.Parse("b2b22222-bbbb-4222-bbbb-2222b2b2b2b2"),
						Name = "Marketing",
						Description = "Handles branding, digital campaigns, and market research."
					},
					new Department
					{
						Id = Guid.Parse("c3c33333-cccc-4333-cccc-3333c3c3c3c3"),
						Name = "Sales",
						Description = "Manages client relationships and drives revenue growth."
					},
					new Department
					{
						Id = Guid.Parse("d4d44444-dddd-4444-dddd-4444d4d4d4d4"),
						Name = "Human Resources",
						Description = "Oversees recruitment, employee well-being and internal policies."
					},
					new Department
					{
						Id = Guid.Parse("e5e55555-eeee-4555-eeee-5555e5e5e5e5"),
						Name = "Finance",
						Description = "Controls budgeting, accounting, and financial reporting."
					},
					new Department
					{
						Id = Guid.Parse("f6f66666-ffff-4666-ffff-6666f6f6f6f6"),
						Name = "Product",
						Description = "Leads product vision, roadmap and customer experience."
					},
					new Department
					{
						Id = Guid.Parse("a9a99999-a9a9-4999-a9a9-9999a9a9a9a9"),
						Name = "Customer Success",
						Description = "Supports clients post-sale and ensures their satisfaction and retention."
					}
				);
			});

			modelBuilder.Entity<Domain.Entities.Position>(entity =>
			{
				entity.HasData(
					new Domain.Entities.Position
					{
						Id = Guid.Parse("10101010-aaaa-4aaa-aaaa-aaaaaaaaaaaa"),
						Title = "Senior .NET Developer",
						Description = "Responsable del desarrollo de servicios backend usando .NET 8 y Azure Functions.",
						Location = "Lima, Perú",
						Status = PositionStatus.Open,
						RecruiterId = Guid.Parse("a1e0a0f3-11a2-4eaa-8bfb-1b4b1f98a111"),
						DepartmentId = Guid.Parse("a1a11111-aaaa-4111-aaaa-1111a1a1a1a1"),
						Budget = 110000,
						ClosingDate = new DateTime(2025, 8, 15)
					},
					new Domain.Entities.Position
					{
						Id = Guid.Parse("20202020-bbbb-4bbb-bbbb-bbbbbbbbbbbb"),
						Title = "UX Designer",
						Description = "Diseño de interfaces centradas en el usuario para la nueva plataforma SaaS.",
						Location = "Remoto - LatAm",
						Status = PositionStatus.Open,
						RecruiterId = Guid.Parse("b2c1b1e4-22b3-4fbb-9cfc-2c5c2f09b222"),
						DepartmentId = Guid.Parse("f6f66666-ffff-4666-ffff-6666f6f6f6f6"),
						Budget = 85000,
						ClosingDate = new DateTime(2025, 8, 30)
					},
					new Domain.Entities.Position
					{
						Id = Guid.Parse("30303030-cccc-4ccc-cccc-cccccccccccc"),
						Title = "Digital Marketing Analyst",
						Description = "Optimización de campañas en Google Ads, SEO y analítica web.",
						Location = "Santiago, Chile",
						Status = PositionStatus.Draft,
						RecruiterId = Guid.Parse("c3d2c2f5-33c4-4bcc-adfd-3d6d3a10c333"),
						DepartmentId = Guid.Parse("b2b22222-bbbb-4222-bbbb-2222b2b2b2b2"),
						Budget = 65000,
						ClosingDate = null
					},
					new Domain.Entities.Position
					{
						Id = Guid.Parse("40404040-dddd-4ddd-dddd-dddddddddddd"),
						Title = "Junior Frontend Developer",
						Description = "Desarrollo de interfaces en React y TypeScript. Ideal para primeros trabajos.",
						Location = "Buenos Aires, Argentina",
						Status = PositionStatus.Open,
						RecruiterId = Guid.Parse("b7f6f6b9-77f8-4bff-bfef-7f0f7a54b777"),
						DepartmentId = Guid.Parse("a1a11111-aaaa-4111-aaaa-1111a1a1a1a1"),
						Budget = 48000,
						ClosingDate = new DateTime(2025, 9, 10)
					},
					new Domain.Entities.Position
					{
						Id = Guid.Parse("50505050-eeee-4eee-eeee-eeeeeeeeeeee"),
						Title = "Accounting Specialist",
						Description = "Gestión contable, conciliaciones y reportes mensuales.",
						Location = "Lima, Perú",
						Status = PositionStatus.Closed,
						RecruiterId = Guid.Parse("f6f5f5a8-66f7-4aff-dfef-6f9f6a43f666"),
						DepartmentId = Guid.Parse("e5e55555-eeee-4555-eeee-5555e5e5e5e5"),
						Budget = 72000,
						ClosingDate = new DateTime(2025, 7, 10)
					},
					new Domain.Entities.Position
					{
						Id = Guid.Parse("60606060-ffff-4fff-ffff-ffffffffffff"),
						Title = "HR Business Partner",
						Description = "Gestión del talento, desarrollo organizacional y clima laboral.",
						Location = "Quito, Ecuador",
						Status = PositionStatus.Archived,
						RecruiterId = Guid.Parse("d4e3d3b6-44d5-4bdd-bece-4e7e4a21d444"),
						DepartmentId = Guid.Parse("d4d44444-dddd-4444-dddd-4444d4d4d4d4"),
						Budget = 90000,
						ClosingDate = new DateTime(2024, 12, 15)
					},
					new Domain.Entities.Position
					{
						Id = Guid.Parse("70707070-aaaa-4aaa-bbbb-bbbbbbaaaaaa"),
						Title = "Sales Executive B2B",
						Description = "Venta de software corporativo a clientes grandes en LatAm.",
						Location = "Remoto - Colombia",
						Status = PositionStatus.Open,
						RecruiterId = Guid.Parse("e5f4e4a7-55e6-4dee-cafe-5f8f5a32e555"),
						DepartmentId = Guid.Parse("c3c33333-cccc-4333-cccc-3333c3c3c3c3"),
						Budget = 100000,
						ClosingDate = null
					}
				);
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}
