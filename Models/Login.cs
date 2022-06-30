using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_teste_dotnet.Models
{
    public class Login
    {
        [Required, DataType(DataType.EmailAddress), JsonPropertyName("E-mail")]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), JsonPropertyName("Senha")]
        public string Senha { get; set; }
    }
}
