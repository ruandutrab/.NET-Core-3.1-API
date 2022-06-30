using api_teste_dotnet.Context;
using api_teste_dotnet.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace api_teste_dotnet.Service
{
    public class UsuariosService : IUsuariosService
    {
        public UsuariosDbContext _UsuariosDbContext;

        public UsuariosService(UsuariosDbContext UsuariosDbContext) => _UsuariosDbContext = UsuariosDbContext;

        public void AddUsuarios(Usuario Usuario)
        {
            var result = _UsuariosDbContext.usuarios.Any(x => x.Email == Usuario.Email);
            if (!result) 
            {
                _UsuariosDbContext.usuarios.Add(Usuario);
                _UsuariosDbContext.SaveChanges();
            } else
            {
                throw new Exception("Usuário já cadastrado!");
            }

        }
        public List<Usuario> GetUsuarios()
        {
            return _UsuariosDbContext.usuarios.ToList();
        }

        public void UpdateUsuarios(Usuario Usuario)
        {
            _UsuariosDbContext.usuarios.Update(Usuario);
            _UsuariosDbContext.SaveChanges();
        }

        public void DeleteUsuarios(int Id)
        {
            var Usuario = _UsuariosDbContext.usuarios.FirstOrDefault(x => x.Id == Id);
            if (Usuario != null)
            {
                _UsuariosDbContext.Remove(Usuario);
                _UsuariosDbContext.SaveChanges();
            }
        }

        public Usuario GetUsuario(int Id) 
        {
            var result = _UsuariosDbContext.usuarios.First(x => x.Id == Id);
            return result;
        }
    }
}
