using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Selu383.SP25.Api.Data;
using Selu383.SP25.Api.Entities; // Ensure this matches your namespace

namespace Selu383.SP25.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            // Swagger implementation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add Database Context
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));

            // Add CORS policy (optional)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger")); // Redirect root URL to Swagger UI only in dev
            }

            // Apply migrations at startup
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                await db.Database.MigrateAsync(); // Apply database migrations automatically

                if (!db.Theaters.Any())
                {
                    db.Theaters.AddRange([
                        new Theater { Name = "Grand Cinema", Address = "123 Main St", SeatCount = 200 },
                        new Theater { Name = "Regal Theater", Address = "456 Elm St", SeatCount = 150 },
                        new Theater { Name = "Majestic Movies", Address = "789 Oak St", SeatCount = 300 }
                    ]);
                    db.SaveChanges();
                }
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAllOrigins"); // Apply CORS policy
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
