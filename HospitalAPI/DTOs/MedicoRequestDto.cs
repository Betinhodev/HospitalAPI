using HospitalAPI.Models;

namespace HospitalAPI.DTOs
{
    public class MedicoRequestDto
    {
        public IFormFile? imgDoc { get; set; }

        public string? CPF { get; set; }

        public string? Password { get; set; }

        public string? Nome { get; set; }

        public string? ImgDocumento { get; set; }
    }
}
