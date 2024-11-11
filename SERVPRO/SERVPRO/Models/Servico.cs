using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace SERVPRO.Models
{
    public class Servico
    {

        [JsonIgnore]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        [JsonIgnore]
        public List<ServicoProduto>? ServicoProdutos { get; set; }
    }
}
