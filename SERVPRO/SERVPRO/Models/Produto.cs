using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SERVPRO.Models
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public string CategoriaProduto { get; set; }
        public decimal CustoInterno { get; set; }
        public decimal CustoVendaCliente { get; set; }
        public decimal? CustoAssociadoServico { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataEntrada { get; set; }


    }
}
