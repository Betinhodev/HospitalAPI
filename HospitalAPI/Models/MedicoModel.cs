namespace HospitalAPI.Models
{
    public class MedicoModel
    {
        public int MedicoId { get; set; }

        public string CPF { get; set; }

        public string Password { get; set; }

        public string? Nome { get; set; }

        public int? ConsultaId { get; set; }
        public ICollection<ConsultaModel> Consulta { get; set; } = [];

        public ICollection<LaudoModel> Laudo { get; set; } = [];

        public ICollection<ExameModel> Exame { get; set; } = [];

        public ICollection<RetornoModel> Retorno { get; set; } = [];

    }
}
