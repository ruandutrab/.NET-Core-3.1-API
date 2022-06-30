using api_teste_dotnet.Models;

namespace api_teste_dotnet.Service
{
    public interface IUsuarioLoginService
    {
        Usuario Logar(Login login);
    }
}
