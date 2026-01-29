using EmployeeManagement.Data;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration de la base de données (InMemory pour simplifier)
            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseInMemoryDatabase("EmployeeDB")
            );

            // Configuration CORS (Angular en local)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    "MyCors",
                    policyBuilder =>
                    {
                        policyBuilder
                            .WithOrigins("http://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    }
                );
            });

            // Injection du Repository (accès aux données)
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // Injection du Service (logique métier)
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            // Activer CORS
            app.UseCors("MyCors");

            // Mapper les controllers
            app.MapControllers();

            app.Run();
        }
    }
}
