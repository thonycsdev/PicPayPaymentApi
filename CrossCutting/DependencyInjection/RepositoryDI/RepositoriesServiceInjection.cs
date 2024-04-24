using Infra.Repositories;
using Infra.RepositoriesInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection.RepositoryDI
{
    public static class RepositoriesServiceInjection
    {
        public static void AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }

}

