using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_teste_dotnet.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
        [JsonIgnore]
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public DateTime DataInsercao { get; set; }
        [JsonIgnore]
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataConclusao { get; set; }
        public byte Concluido { get; set; }
        public string UserEmail { get; set; }
    }

}
