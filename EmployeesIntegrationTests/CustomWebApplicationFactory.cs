using Employees.Server.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace EmployeesIntegrationTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IDisposable where TProgram : class
    {
        public DbContextOptions<EmployeeContext> Options { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                ServiceDescriptor? dbContextDescriptor = services.SingleOrDefault(
                   d => d.ServiceType ==
                       typeof(DbContextOptions<EmployeeContext>));
                services.Remove(dbContextDescriptor);

                IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
                string connection = configuration["ConnectionStringTest"];

                DbContextOptionsBuilder<EmployeeContext> optionsBuilder = new DbContextOptionsBuilder<EmployeeContext>();
                optionsBuilder.UseSqlServer(connection);
                services.AddScoped((pr) => optionsBuilder.Options);

                Options = optionsBuilder.Options;
            });
        }
    }

    public class DatabaseFixture<T> : CustomWebApplicationFactory<T>, IDisposable where T : class
    {
        public void Dispose()
        {
            EmployeeContext context = new EmployeeContext(Options);
            context.Database.EnsureDeleted();
            base.Dispose();
        }
    }
}
