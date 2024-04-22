using HospitalAPI.Models;

namespace HospitalAPI.DTOs
{
    public class MedicoRequestDto : MedicoModel
    {
        public IFormFile? imgDoc { get; set; }
    }
}
