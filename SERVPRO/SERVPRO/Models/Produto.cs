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
        public int IdProduto { get; set; }

        public string NomeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public string CategoriaProduto { get; set; }
        public decimal PrecoProduto { get; set; }
        public int QtdProduto { get; set; }
        public DateTime DataEntrada { get; set; }


    }
}
