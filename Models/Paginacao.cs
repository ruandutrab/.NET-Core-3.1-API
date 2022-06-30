
using api_teste_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace PaginacaoWeb.Paginacao
{
    public static class Paginacao
    {
        public static PagedResult<Tarefa> GetPaged<T>(this IQueryable<Tarefa> query,
                                                     int page, int pageSize, int userId, string level) where T : class
        {
            var result = new PagedResult<Tarefa>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            if (level == "user") result.RowCount = query.Where(x => x.UsuarioId == userId).Count();
            else result.RowCount = query.Count();


            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            if (level == "user") result.Results = query.Where(x => x.UsuarioId == userId).Skip(skip).Take(pageSize).ToList();
            else result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}