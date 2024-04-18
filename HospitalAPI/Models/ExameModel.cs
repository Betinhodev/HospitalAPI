    using HospitalAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Models
{
    public class ExameModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Tipo {  get; set; }

        public int MedicoResponsavelId { get; set; }

        public MedicoModel MedicoResponsavel { get; set; } = null!;

        public int PacienteId {  get; set; }

        public PacienteModel Paciente { get; set; } = null!;

        public DateTime DataAgendamento {  get; set; }

        public decimal Valor {  get; set; }

        public string? Descricao {  get; set; }

        public StatusExame Status {  get; set; }

    }
}
