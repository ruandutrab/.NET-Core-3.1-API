using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_teste_dotnet.Models
{
    public class Usuario
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [EmailAddress, Required, JsonPropertyName("E-mail")]
        public string Email { get; set; }
        [Required, JsonPropertyName("Senha")]
        public string Senha { get; set; }
        [Required, JsonPropertyName("Level: (user ou admin)")]
        public string Level { get; set; }
    }
}
