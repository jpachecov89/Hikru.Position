using Hikru.Position.Backend.Api.Middlewares;
using Hikru.Position.Backend.Application.Interfaces.Authentication;
using Hikru.Position.Backend.Application.Interfaces.Persistence;
using Hikru.Position.Backend.Application.Positions.Commands.CreatePosition;
using Hikru.Position.Backend.Infrastructure.Authentication;
using Hikru.Position.Backend.Infrastructure.Persistence;
using Hikru.Position.Backend.Infrastructure.Persistence.Contexts;
using Hikru.Position.Backend.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Hikru.Position.Backend.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IHikruJwtSecurityToken, HikruJwtSecurityToken>();

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IRecruiterRepository, RecruiterRepository>();
            builder.Services.AddScoped<IPositionRepository, PositionRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreatePositionCommand).Assembly);
            });

			builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new() { Title = "Hikru", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Join token JWT in this format: Bearer {token}"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
						},
						Array.Empty<string>()
					}
				});
			});

			var app = builder.Build();

			app.UseCors(c => c
			.WithOrigins("http://localhost:3000", "https://agreeable-rock-05d508c1e.1.azurestaticapps.net")
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowCredentials()
			.WithExposedHeaders("location")
			.WithExposedHeaders("content-disposition")
			);

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMiddleware<JwtAuthorizationMiddleware>();
			app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
