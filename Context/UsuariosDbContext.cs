using api_teste_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace api_teste_dotnet.Context
{
    public class UsuariosDbContext : DbContext
    {
        public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> usuarios { get; set; }
    }
}
