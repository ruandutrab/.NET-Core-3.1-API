using api_teste_dotnet.Models;
using System.Collections.Generic;

namespace api_teste_dotnet.Service
{
    public interface IUsuariosService
    {
        void AddUsuarios(Usuario Usuario);
        List<Usuario> GetUsuarios();
        void UpdateUsuarios(Usuario Usuario);
        void DeleteUsuarios(int Id);
        Usuario GetUsuario(int Id);
    }
}
