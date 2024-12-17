using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using To_Do_List.BLL.Repositories.Interfaces;
using To_Do_List.BLL.Repositories.Repositories;
using To_Do_List.DAL.Models;
using To_Do_List.DAL.Models.Context;
using To_Do_List.Helpers;


namespace To_Do_List
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            // Add DbContext with the MigrationsAssembly
            builder.Services.AddDbContext<ToDoListContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoListDB"),
                    b => b.MigrationsAssembly("To_Do_List.DAL")));  // Specify the migrations assembly here

            builder.Services.AddScoped<IGenericRepository<Tasks>, GenericReposiory<Tasks>>();


            // Add Swagger for API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            await RunSeedingAsync(services);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        private static async Task RunSeedingAsync(IServiceProvider services)
        {
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            try
            {
                var dbContext = services.GetRequiredService<ToDoListContext>();
                await dbContext.Database.MigrateAsync();


                logger.LogInformation("Database migration completed.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during database migration: {Message}", ex.Message);
                throw;
            }
        }
    }
}

