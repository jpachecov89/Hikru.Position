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
						Id = Guid.Parse("7A79F0C2-F8FD-4A59-BA0C-5096FE7B81FE"),
						Name = "Engineering",
						Description = "Responsible for software development, architecture and technical innovation."
					},
					new Department
					{
						Id = Guid.Parse("9A3BA9C6-2549-485E-B111-4A0FFC60C19A"),
						Name = "Marketing",
						Description = "Handles branding, digital campaigns, and market research."
					},
					new Department
					{
						Id = Guid.Parse("9F45C02F-DFE2-476D-9FFB-4FDA843E8181"),
						Name = "Sales",
						Description = "Manages client relationships and drives revenue growth."
					},
					new Department
					{
						Id = Guid.Parse("FF722DD0-1FAC-4B5F-8D07-77E43D82FE1F"),
						Name = "Human Resources",
						Description = "Oversees recruitment, employee well-being and internal policies."
					},
					new Department
					{
						Id = Guid.Parse("3D2897ED-1B11-4F99-B26A-92A3BAEA5A79"),
						Name = "Finance",
						Description = "Controls budgeting, accounting, and financial reporting."
					},
					new Department
					{
						Id = Guid.Parse("9BB98DBD-1B21-494A-A9A5-6B7E3967A5DC"),
						Name = "Product",
						Description = "Leads product vision, roadmap and customer experience."
					},
					new Department
					{
						Id = Guid.Parse("510A002E-510A-490C-8433-0808BC0215B3"),
						Name = "Customer Success",
						Description = "Supports clients post-sale and ensures their satisfaction and retention."
					}
				);
			});

			modelBuilder.Entity<Domain.Entities.Position>(entity =>
			{
				entity.HasData(
					new Domain.Entities.Position
					(
						"Senior .NET Developer",
						"Responsible for the development of backend services using .NET 8 and Azure Functions.",
						"Lima, Perú",
						PositionStatus.Open,
						Guid.Parse("a1e0a0f3-11a2-4eaa-8bfb-1b4b1f98a111"),
						Guid.Parse("7A79F0C2-F8FD-4A59-BA0C-5096FE7B81FE"),
						110000,
						new DateTime(2025, 8, 15)
					),
					new Domain.Entities.Position
					(
						"UX Designer",
						"Designing user-centric interfaces for the new SaaS platform.",
						"Remote - LatAm",
						PositionStatus.Open,
						Guid.Parse("b2c1b1e4-22b3-4fbb-9cfc-2c5c2f09b222"),
						Guid.Parse("9BB98DBD-1B21-494A-A9A5-6B7E3967A5DC"),
						85000,
						new DateTime(2025, 8, 30)
					),
					new Domain.Entities.Position
					(
						"Digital Marketing Analyst",
						"Google Ads campaign optimization, SEO, and web analytics.",
						"Santiago, Chile",
						PositionStatus.Draft,
						Guid.Parse("c3d2c2f5-33c4-4bcc-adfd-3d6d3a10c333"),
						Guid.Parse("9A3BA9C6-2549-485E-B111-4A0FFC60C19A"),
						65000,
						null
					),
					new Domain.Entities.Position
					(
						"Junior Frontend Developer",
						"Interface development in React and TypeScript. Ideal for beginners.",
						"Buenos Aires, Argentina",
						PositionStatus.Open,
						Guid.Parse("b7f6f6b9-77f8-4bff-bfef-7f0f7a54b777"),
						Guid.Parse("7A79F0C2-F8FD-4A59-BA0C-5096FE7B81FE"),
						48000,
						new DateTime(2025, 9, 10)
					),
					new Domain.Entities.Position
					(
						"Accounting Specialist",
						"Accounting management, reconciliations and monthly reports.",
						"Lima, Perú",
						PositionStatus.Closed,
						Guid.Parse("f6f5f5a8-66f7-4aff-dfef-6f9f6a43f666"),
						Guid.Parse("3D2897ED-1B11-4F99-B26A-92A3BAEA5A79"),
						72000,
						new DateTime(2025, 7, 10)
					),
					new Domain.Entities.Position
					(
						"HR Business Partner",
						"Talent management, organizational development and work environment.",
						"Quito, Ecuador",
						PositionStatus.Archived,
						Guid.Parse("d4e3d3b6-44d5-4bdd-bece-4e7e4a21d444"),
						Guid.Parse("FF722DD0-1FAC-4B5F-8D07-77E43D82FE1F"),
						90000,
						new DateTime(2024, 12, 15)
					),
					new Domain.Entities.Position
					(
						"Sales Executive B2B",
						"Sales of corporate software to large clients in Latin America.",
						"Remote - LatAm",
						PositionStatus.Open,
						Guid.Parse("e5f4e4a7-55e6-4dee-cafe-5f8f5a32e555"),
						Guid.Parse("9F45C02F-DFE2-476D-9FFB-4FDA843E8181"),
						100000,
						null
					)
				);
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}
