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

        public AuthController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult Auth([FromForm] AuthRequest auth)
        {
           
            var token = _authenticationService.AuthenticateUser(auth.CPF, auth.Password);
            
            if(token == null)
            return BadRequest("usuário ou senha incorreta.");

            return Ok(new { token });
        }
    }
}
