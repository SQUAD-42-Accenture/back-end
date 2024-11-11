using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SERVPRO.Models
{
    public class ServicoProduto
    {
      
        [JsonIgnore]
        public int Id { get; set; }
        public int ServicoId { get; set; }
        public int ProdutoId { get; set; }

        public decimal CustoProdutoNoServico { get; set; }

        public int OrdemDeServicoId { get; set; }

        [JsonIgnore]
        public Servico? Servico { get; set; }

        [JsonIgnore]
        public Produto? Produto { get; set; }

        [JsonIgnore]
        public OrdemDeServico? OrdemDeServico { get; set; }
    }
}
