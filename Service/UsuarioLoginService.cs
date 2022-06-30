using api_teste_dotnet.Context;
using api_teste_dotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_teste_dotnet.Service
{
    public class UsuarioLoginService : IUsuarioLoginService
    {
        public UsuariosDbContext _usuariosDbContext;

        public UsuarioLoginService(UsuariosDbContext usuariosDbContext) => _usuariosDbContext = usuariosDbContext;
        public Usuario Logar(Login login)
        {
            bool validLogin = _usuariosDbContext.usuarios.Any(user => user.Email == login.Email && user.Senha == login.Senha);

            if (validLogin)
            {
                var result = _usuariosDbContext.usuarios.First(x => x.Email == login.Email);
                return result;

            }
            return null;
        }
    }
}
