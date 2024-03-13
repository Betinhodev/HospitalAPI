using HospitalAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Models
{
    public class RetornoModel
    {
        [Key]
        public int RetornoId { get; set; }
        public int MedicoId { get; set; }
        public DateTime Data { get ; set ; }
        public StatusRetorno Status { get; set; }
        public int PacienteId { get; set; }
        public Guid ConsultaId { get; set; }

    }
}
