using SERVPRO.Enums;
using System.Text.Json.Serialization;
namespace SERVPRO.Models
{
    public class HistoricoOS
    {
        public int Id { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string Comentario { get; set; }

        public int? OrdemDeServicoId { get; set; }
        [JsonIgnore]
        public virtual OrdemDeServico? OrdemDeServico { get; set; }

        public string? TecnicoCPF { get; set; }
        [JsonIgnore]
        public virtual Tecnico? Tecnico { get; set; }
    }

}