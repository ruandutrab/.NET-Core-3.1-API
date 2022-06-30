using api_teste_dotnet.Models;
using api_teste_dotnet.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Linq;
using PaginacaoWeb.Paginacao;

namespace api_teste_dotnet.Controllers
{
    [Route("[controller]")]
    public class TarefaController : Controller
    {
        private readonly ITarefasService _tarefas;
        private readonly IConfiguration _config;
        public TarefaController(ITarefasService tarefa, IConfiguration config)
        {
            _tarefas = tarefa;
            _config = config;
        }


        /// <summary>
        /// Controller responsável por pegar todas as tarefas paginadas.
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize(Roles = "user, admin")]
        [Route("ObterTarefas")]
        public PagedResult<Tarefa> Get([FromQuery] int Page = 1, int QuantidadePorPage = 10)
        {
            var userId = User.Identity.GetUserId();
            var auth = User.FindFirstValue(ClaimTypes.Role);
            return _tarefas.ObterTarefas(Convert.ToInt32(userId), auth, Page, QuantidadePorPage);
        }

        /// <summary>
        ///  Controller responsável por chamar o serviço de criação.
        /// </summary>
        [HttpPost, Authorize(Roles = "user, admin"), Route("AdicionarTarefa")]
        public IActionResult AdicionarTarefa([FromBody] Tarefa tarefa)
        {
            tarefa.UserEmail = User.FindFirstValue(ClaimTypes.Email);
            tarefa.DataInsercao = DateTime.Now;
            tarefa.UsuarioId = Convert.ToInt32(User.Identity.GetUserId());
            var result = _tarefas.CriarTarefa(tarefa);

            switch (result)
            {
                case 1:
                    return NotFound("Data de conclusão não pode ser menor que a data de hoje.");
                    break;
                case 2:
                    return NotFound("Você não pode criar uma tarefa como concluída!");
                    break;
                default:
                    return Ok("Sua tarefa foi criada com sucesso.");
                    break;
            }

        }

        /// <summary>
        /// Controller responsável por chamar o serviço de edição.
        /// </summary>
        [HttpPut, Authorize(Roles = "user, admin"), Route("EditarTarefa")]
        public IActionResult EditarTarefa([FromBody] Tarefa tarefa)
        {
            var userId = Convert.ToInt32(User.Identity.GetUserId());
            var result = _tarefas.EditarTarefa(tarefa, userId);
            switch (result)
            {
                case 0 :
                    return Ok("Tarefa editada com sucesso.");
                    break;

                case 1 : 
                    return NotFound("Você não pode editar uma tarefa concluída.");
                    break;
                
                case 2 :
                    return NotFound("Essa tarefa não existe ou data de conclusão é inválida!");
                    break;

                default:
                    return BadRequest("Ocorreu um erro ao editar a tarefa!");
                    break;
            }

        }

    }
}
