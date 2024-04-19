using HospitalAPI.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalAPI.Services
{
    public class AuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly HospitalAPIContext _context;

        public AuthenticationService(IConfiguration configuration, HospitalAPIContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public object AuthenticateUser(string username, string password)
        {
            
            var patient = _context.Pacientes.FirstOrDefault(p => p.CPF == username && p.Password == password);
            if (patient != null)
            {
                
                return GenerateToken(patient.PacienteId, "paciente");
            }

            
            var doctor = _context.Medicos.FirstOrDefault(d => d.CPF == username && d.Password == password);
            if (doctor != null)
            {

                return GenerateToken(doctor.MedicoId, "medico");
            }

            
            if (username == "admin" && password == "root")
            {

                return GenerateToken(1, "admin");
            }


            return null;
        }

        public class Key
        {
            public static string Secret = "123456asd974as56af1a6f1as65f4f65a";

        }
        public static object GenerateToken(int userId, string role)
        {
            var key = Encoding.ASCII.GetBytes(Key.Secret);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };
        }
    }
}
