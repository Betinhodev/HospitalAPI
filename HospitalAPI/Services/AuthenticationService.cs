using HospitalAPI.Data;
using HospitalAPI.DTOs;
using HospitalAPI.Models;
using HospitalAPI.Repositorios;
using HospitalAPI.Repositorios.Interfaces;
using HospitalAPI.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly ILogger<AuthenticationService> _logger;
        PassHasher<PacienteModel> hashedPatientPass = new PassHasher<PacienteModel>();
        PassHasher<MedicoModel> hashedDoctorPass = new PassHasher<MedicoModel>();

        public AuthenticationService(IConfiguration configuration, HospitalAPIContext context, ILogger<AuthenticationService> logger)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
        }

        public object AuthenticateUser(AuthRequest authRequest)
        {

            var patient = _context.Pacientes.FirstOrDefault(x => x.CPF ==  authRequest.CPF);
            
            if (patient != null)
            {
                var isValidPass = hashedPatientPass.VerifyHashedPassword(patient, patient.Password, authRequest.Password);
                if (isValidPass == PasswordVerificationResult.Success)
                {
                    _logger.LogInformation($"Paciente {patient.Nome} autenticado.");
                    return GenerateToken(patient.PacienteId, "paciente");
                }
            }


            var doctor = _context.Medicos.FirstOrDefault(x => x.CPF == authRequest.CPF);
            if (doctor != null)
            {
                var isValidPass = hashedDoctorPass.VerifyHashedPassword(doctor, doctor.Password, authRequest.Password);
                if (isValidPass == PasswordVerificationResult.Success)
                {
                    _logger.LogInformation($"Doutor {doctor.Nome} autenticado.");
                    return GenerateToken(doctor.MedicoId, "medico");

                }
            }
            
            if (authRequest.CPF == "admin" && authRequest.Password == "root")
            {
                _logger.LogInformation($"Admin autenticado.");
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
