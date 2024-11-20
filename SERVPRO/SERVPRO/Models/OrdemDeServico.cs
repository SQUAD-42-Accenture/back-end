using SERVPRO.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SERVPRO.Models
{
    public class OrdemDeServico
    {
        [Key]  // Chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Geração automática de ID pelo banco
        public int Id { get; set; }
        public DateTime dataAbertura { get; set; }
        public DateTime? dataConclusao { get; set; }
        public string Descricao { get; set; }
        public string? MetodoPagamento { get; set; }

        public decimal? ValorTotal { get; set; }


        public string? ClienteCPF { get; set; }
        [JsonIgnore]
        public virtual Cliente? Cliente { get; set; }

        public string? SerialEquipamento { get; set; }
        [JsonIgnore]
        public virtual Equipamento? Equipamento { get; set; }

        public string? TecnicoCPF { get; set; }
        [JsonIgnore]
        public virtual Tecnico? Tecnico { get; set; }

        public string Status { get; set; }
        [JsonIgnore]
        public List<HistoricoOS>? Historicos { get; set; }
        
        public virtual List<ServicoProduto>? ServicoProdutos { get; set; }  // Relacionamento com ServicoProduto

      

    }

}
