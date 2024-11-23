using System.Text.Json.Serialization;

namespace SERVPRO.Models
{
    public class Tecnico : Usuario
    {

        [JsonPropertyOrder(6)]
        public string Especialidade { get; set; }

        [JsonPropertyOrder(7)]
        public string Telefone { get; set; }

        [JsonPropertyOrder(8)]
        public string StatusTecnico { get; set; }

        [JsonIgnore]
        public List<OrdemDeServico>? OrdensDeServico { get; set; }

    }

}