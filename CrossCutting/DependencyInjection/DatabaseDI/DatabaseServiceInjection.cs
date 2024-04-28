using Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection.DatabaseDI
{
    public static class DatabaseServiceInjection
    {
        public static void AddDatabase(
            this IServiceCollection service,
            IConfiguration configuration
        )
        {
            service.AddDbContext<DatabaseContext>(x =>
                x.UseNpgsql(configuration.GetConnectionString("Local_Database"))
            );
        }
    }
}
