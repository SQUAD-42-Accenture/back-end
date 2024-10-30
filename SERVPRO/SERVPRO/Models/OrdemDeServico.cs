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

        public string? ClienteCPF { get; set; }
        public virtual Cliente? Cliente { get; set; }

        public string? SerialEquipamento { get; set; }
        public virtual Equipamento? Equipamento { get; set; }

        public string? TecnicoCPF { get; set; }
        public virtual Tecnico? Tecnico { get; set; }


        public StatusOS Status { get; set; }
        [JsonIgnore]
        public List<HistoricoOS> Historicos { get; set; }
    }

}
