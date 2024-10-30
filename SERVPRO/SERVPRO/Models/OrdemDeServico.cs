using SERVPRO.Enums;
using System.Text.Json.Serialization;

namespace SERVPRO.Models
{
    public class OrdemDeServico
    {
        public int Id { get; set; }
        public DateTime dataAbertura { get; set; }
        public DateTime? dataConclusao { get; set; }
        public string Descricao { get; set; }
        public string? MetodoPagamento { get; set; }
        public string? ValorTotal { get; set; }


        public string? ClienteCPF { get; set; }
        [JsonIgnore]
        public virtual Cliente? Cliente { get; set; }

        public string? SerialEquipamento { get; set; }
        [JsonIgnore]
        public virtual Equipamento? Equipamento { get; set; }

        public string? TecnicoCPF { get; set; }
        [JsonIgnore]
        public virtual Tecnico? Tecnico { get; set; }

        public string? IdProduto { get; set; }
        public virtual Produto? Produto { get; set; }


        public string Status { get; set; }
        [JsonIgnore]
        public List<HistoricoOS> Historicos { get; set; }
    }

}
