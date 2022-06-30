using api_teste_dotnet.Context;
using api_teste_dotnet.Models;
using api_teste_dotnet.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace api_teste_dotnet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SegurancaController : Controller
    {
        private readonly IUsuarioLoginService _login;
        private readonly IConfiguration _config;

        public SegurancaController(IConfiguration Configuration, IUsuarioLoginService login)
        {
            _config = Configuration;
            _login = login;
        }

        /// <summary>
        /// Realizar o login do usuário.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost, Route("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            var resultado = _login.Logar(login);
            if (resultado != null && !string.IsNullOrEmpty(resultado.Email))
            {
                var tokenString = GerarTokenJWT(resultado);
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Método responsável por gerar um toke de acesso.
        /// </summary>
        /// <param name="user"> Contem os dados do usuário logado para salvar nas Claims.</param>
        /// <returns>Token de acesso.</returns>
        private string GerarTokenJWT(Usuario user)
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expireMinutes = Convert.ToInt32(_config["Jwt:ExpireMinutes"]);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Level),
                    };

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer, audience: audience, expires: DateTime.Now.AddHours(expireMinutes), signingCredentials: credentials, claims: claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return $"Bearer {stringToken}";
        }


    }
}
