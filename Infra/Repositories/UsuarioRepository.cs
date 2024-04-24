using Domain.Entities;
using Infra.RepositoriesInterfaces;

namespace Infra.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DatabaseContext context) : base(context) { }
    }
}
