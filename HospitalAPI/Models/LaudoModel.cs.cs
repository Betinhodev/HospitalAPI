using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Models
{
    public class LaudoModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    
        public string? Descrição { get; set; }

        public int PacienteId { get; set; }

        public PacienteModel Paciente { get; set; } = null!;

        public int MedicoId { get; set; }

        public MedicoModel Medico { get; set; } = null!;

        public DateTime DataCriacao { get; set; }

        public Guid ConsultaId {  get; set; }

    }
}
