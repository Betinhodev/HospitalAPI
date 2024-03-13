using HospitalAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Models
{
    public class ConsultaModel
    {

        [Key]
        public Guid ConsultaId { get; set; }
        public int MedicoId { get; set; }
        public DateTime DataDoCadastro { get ; set ; }
        public StatusConsulta Status { get; set; }
        public virtual MedicoModel? Medico { get; set; }
        public int PacienteId { get; set; }
        public virtual PacienteModel? Paciente { get; set; }

        public decimal Valor {  get; set; }

    }
}
