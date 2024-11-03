using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SERVPRO.Models
{
    public class ServicoProduto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        public int ServicoId { get; set; }
        public int ProdutoId { get; set; }
        public decimal PrecoAdicional { get; set; }


        [ForeignKey("ServicoId")]
        [JsonIgnore]
        public Servico? Servico { get; set; }

        [ForeignKey("ProdutoId")]
        [JsonIgnore]
        public Produto? Produto { get; set; }
    }
}
