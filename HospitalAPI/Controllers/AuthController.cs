using HospitalAPI.DTOs;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthenticationService authenticationService, ILogger<AuthController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Auth([FromForm] AuthRequest auth)
        {
           
            var token = _authenticationService.AuthenticateUser(auth.CPF, auth.Password);

            if (token == null)
            {
                _logger.LogWarning("usuário ou senha incorreta.");
                return BadRequest("usuário ou senha incorreta.");
            }

            return Ok(new { token });
        }
    }
}
