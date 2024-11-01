using System.Text.Json.Serialization;
namespace SERVPRO.Models
{
    public class Cliente : Usuario
    {
        [JsonPropertyOrder(6)]
        public string Telefone { get; set; }
        [JsonPropertyOrder(7)]
        public DateTime? DataNascimento { get; set; }
        [JsonPropertyOrder(8)]
        public string Bairro { get; set; }
        [JsonPropertyOrder(9)]
        public string Cidade { get; set; }
        [JsonPropertyOrder(10)]
        public string CEP { get; set; }
        [JsonPropertyOrder(11)]
        public string Complemento { get; set; }

        [JsonIgnore]
        public List<Equipamento>? Equipamentos { get; set; }
        [JsonIgnore]
        public List<OrdemDeServico>? OrdensDeServico { get; set; }
    }

}