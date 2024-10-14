using System.ComponentModel;

namespace SERVPRO.Enums
{
    public enum TipoUsuario
    {
        [Description("Cliente")]
        Cliente = 0,

        [Description("Tecnico")]
        Tecnico = 1,

        [Description("Administrador")]
        Administrador = 2
      
    }
}
