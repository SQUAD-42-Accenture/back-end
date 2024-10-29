using System.Text.Json.Serialization;
namespace SERVPRO.Models
{
    public class Cliente: Usuario
    {
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }
        public string Complemento { get; set; }

        [JsonIgnore]
        public List<Equipamento>? Equipamentos { get; set; }
        [JsonIgnore]
        public List<OrdemDeServico>? OrdensDeServico { get; set; }
    }

}
