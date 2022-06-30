using api_teste_dotnet.Models;
using api_teste_dotnet.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace api_teste_dotnet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {

        private readonly IUsuariosService _Usuarioservice;
        public UsuariosController(IUsuariosService Usuarioservice) => _Usuarioservice = Usuarioservice;
        
        /// <summary>
        /// Pega todos os usuários cadastrados no sistema.
        /// </summary>
        /// <returns>Retorna uma lista de usuários.</returns>
        [HttpGet, Authorize(Roles = "admin")]
        [Route("GetUsuarios")]
        public IEnumerable<Usuario> GetUsuarios()
        {
            return _Usuarioservice.GetUsuarios();
        }

        /// <summary>
        /// Adiciona um novo usuário ao sistema.
        /// </summary>
        /// <param name="Usuario"> Dados do usuário.</param>
        [HttpPost]
        [AllowAnonymous]
        [Route("AddUsuarios")]
        public IActionResult AddUsuarios(Usuario usuario)
        {

            if (usuario.Level.Contains("user") || usuario.Level.Contains("admin"))
            {
                _Usuarioservice.AddUsuarios(usuario);

            }
            else
            {
                return BadRequest("O level informado é inválido.");
            }
            return Ok("Usuário cadastrado com sucesso.");
        }

        //Caso seja necessario editar, delete ou atualizar usuário.
        #region
        //[HttpPost]
        //[Authorize]
        //[Route("UpdateUsuarios")]
        //public IActionResult UpdateUsuarios(Usuario Usuario)
        //{
        //    _Usuarioservice.UpdateUsuarios(Usuario);
        //    return Ok();
        //}

        //[HttpDelete]
        //[Authorize]
        //[Route("DeleteUsuarios")]
        //public IActionResult DeleteUsuarios(int id)
        //{
        //    var isUsuario = _Usuarioservice.GetUsuario(id);
        //    if (isUsuario != null)
        //    {
        //        _Usuarioservice.DeleteUsuarios(isUsuario.Id);
        //        return Ok();
        //    }
        //    return NotFound($"Usuario com ID não localizado: {isUsuario.Id}");
        //}

        //[HttpGet]
        //[Authorize]
        //[Route("GetUsuario")]
        //public Usuario GetUsuario(int id)
        //{
        //    return _Usuarioservice.GetUsuario(id);
        //}
        #endregion
    }
}
