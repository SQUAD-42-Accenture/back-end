namespace SERVPRO.Models
{
    public class Equipamento
    {
        public string Serial { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Descricao { get; set; }  
        public DateTime DataCadastro { get; set; }
        public string? ClienteCPF { get; set; }
        public virtual Cliente? Cliente { get; set; }
    }

}
