using HospitalAPI.Models;

namespace HospitalAPI.DTOs
{
    public class PacienteRequestDto : PacienteModel
    {
        public IFormFile? imgDoc { get; set; }
    }
}
