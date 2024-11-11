using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SERVPRO.Models
{
    public class Produto
    {

        [JsonIgnore]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria{ get; set; }
        public decimal CustoInterno { get; set; }
        public decimal CustoVenda { get; set; }
        public DateTime DataEntrada { get; set; }
        public int Quantidade { get; set; }

        public List<ServicoProduto>? ServicoProdutos { get; set; }

    }
}
