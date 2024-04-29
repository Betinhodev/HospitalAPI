using HospitalAPI.Models;

namespace HospitalAPI.DTOs
{
    public class PatientRegisterDto : PacienteModel
    {
        public string? CPF { get; set; }

        public string? Password { get; set; }

        public string? Nome { get; set; }

        public string? Endereco { get; set; }

        public string? DataDeNascimento { get; set; }
        public string? ImgDocumento { get; set; }

        public bool TemConvenio { get; set; }

        public int? ConvenioId { get; set; }
    }
}
