using api_teste_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace api_teste_dotnet.Context
{
    public class TarefasDbContext : DbContext
    {
        public TarefasDbContext(DbContextOptions<TarefasDbContext> options) : base(options)
        {
        }

        public DbSet<Tarefa> tarefas { get; set; }
    }
}
