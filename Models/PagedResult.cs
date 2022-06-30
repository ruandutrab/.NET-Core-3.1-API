
using api_teste_dotnet.Models;
using System.Collections.Generic;
namespace PaginacaoWeb.Paginacao
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }
        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}