﻿using System.ComponentModel;

namespace HospitalAPI.Enums
{
    public enum StatusConsulta
    {
        [Description("Consulta Agendada")]
        Agendada = 1,
        [Description("Consulta Realizada")]
        Realizada = 2,
        [Description("Consulta Cancelada")]
        Expirada = 3,

    }
}
