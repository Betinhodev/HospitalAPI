using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Models
{
    public class ConvenioModel
    {
        [Key]
        public int ConvenioId { get; set; }
        public string? Nome { get; set; }
    }
}
