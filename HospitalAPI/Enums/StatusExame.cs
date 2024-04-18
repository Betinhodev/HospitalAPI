using System.ComponentModel;

namespace HospitalAPI.Enums
{
    public enum StatusExame
    {
        [Description("Exame Agendado")]
        Agendada = 1,
        [Description("Exame Realizado")]
        Realizada = 2,
        [Description("Exame Cancelado")]
        Expirada = 3,
    }
}
