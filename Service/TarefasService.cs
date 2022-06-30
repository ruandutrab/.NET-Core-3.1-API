using api_teste_dotnet.Context;
using api_teste_dotnet.Models;
using PaginacaoWeb.Paginacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace api_teste_dotnet.Service
{
    public class TarefasService : ITarefasService
    {
        public TarefasDbContext _UsuariosDbContext;

        public TarefasService(TarefasDbContext TarefasDbContext) => _UsuariosDbContext = TarefasDbContext;

        public int CriarTarefa(Tarefa tarefa)
        {
            try
            {
                if (tarefa.Concluido == 0)
                {
                    if (tarefa.DataConclusao > DateTime.Now)
                    {
                        _UsuariosDbContext.tarefas.Add(tarefa);
                        _UsuariosDbContext.SaveChanges();
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return 2;
                }
            }
            catch (Exception x)
            {

                throw(x);
            }
            return 0;
        }

        public PagedResult<Tarefa> ObterTarefas(int userId, string level, int page, int qtdPage)
        {
            if (page <= 0 || qtdPage <= 0)
            {
                page = 1;
                qtdPage = 10;
            }
            var dbDados = _UsuariosDbContext.tarefas.GetPaged<Tarefa>(page, qtdPage, userId, level);
            int totalVencido = dbDados.Results.Where(x => x.DataConclusao < DateTime.Now).Count();

            dbDados.TotalOverdue = totalVencido;
            return dbDados;
        }

        public int EditarTarefa(Tarefa tarefa, int userId)
        {
            try
            {
                List<Tarefa> obterTarefa = _UsuariosDbContext.tarefas.Where(x => x.Id == tarefa.Id).ToList();
                foreach (var item in obterTarefa)
                {
                    if (item.UsuarioId == userId && item.DataConclusao > DateTime.Now)
                    {
                        if (item.Concluido == 0)
                        {
                            item.Descricao = tarefa.Descricao;
                            item.Concluido = tarefa.Concluido;
                            item.DataConclusao = tarefa.DataConclusao;
                            item.DataAtualizacao = DateTime.Now;
                        }
                        else
                        {
                            return 1;
                        }
                        tarefa = item;
                        _UsuariosDbContext.tarefas.Update(tarefa);
                        _UsuariosDbContext.SaveChanges();
                        return 0;
                    }
                    else
                    {
                        return 2;
                    }
                        
                }
            }
            catch (Exception x)
            {

                throw(x);
            }
            return 3;
        }
    }
}
