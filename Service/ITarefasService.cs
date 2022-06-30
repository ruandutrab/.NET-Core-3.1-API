

using api_teste_dotnet.Models;
using PaginacaoWeb.Paginacao;


namespace api_teste_dotnet.Service
{
    public interface ITarefasService
    {
        PagedResult<Tarefa> ObterTarefas(int userId, string auth, int page, int qtdPage);
        int CriarTarefa(Tarefa tarefa);
        int EditarTarefa(Tarefa tarefa, int userId);
    }
}
