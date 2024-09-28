
using Employees.Server.Data;
using Employees.Server.Data.Repositories;
using Employees.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace Employees.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddDbContext<EmployeeContext>(options =>
               options.UseSqlServer(builder.Configuration["ConnectionString"])
                      .EnableDetailedErrors()
                      .EnableSensitiveDataLogging()
                      .LogTo(
                          Console.WriteLine,
                          new[] { DbLoggerCategory.Database.Command.Name },
                          LogLevel.Information));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(policyBuilder =>
                policyBuilder.AddDefaultPolicy(policy =>
                        policy.WithOrigins("*")
                           .AllowAnyHeader()
                           .AllowAnyMethod())
            );


            builder.Services.AddTransient<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddTransient<IEmployeeMapper, EmployeeMapper>();

            var app = builder.Build();

            app.UseCors();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
