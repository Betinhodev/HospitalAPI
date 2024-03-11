namespace HospitalAPI.Models
{
    public class PacienteModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int CPF { get; set; }
        public string Endereco { get; set; }

        public bool TemConvenio {  get; set; }

        public string Convenio {  get; set; }

        public string ImgCarteiraDoConvenio { get; set; }

        public string ImgDocumento { get; set; }


    }
}
