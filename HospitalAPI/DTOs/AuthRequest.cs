using Microsoft.IdentityModel.Tokens;

namespace HospitalAPI.DTOs
{
    public class AuthRequest
    {
        public string CPF { get; set; }

        public string Password { get; set; }
    }
}
