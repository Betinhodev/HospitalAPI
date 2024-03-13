namespace HospitalAPI.Models
{
    public class MedicoModel
    {
        public int MedicoId { get; set; }

        public string? Nome { get; set; }

        public int? ConsultaId { get; set; }
        public ICollection<ConsultaModel>? Consulta { get; set; }

    }
}
