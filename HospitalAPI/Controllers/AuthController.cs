using HospitalAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "medicoadm" && password == "123456")
            {
                var token = TokenService.GenerateToken(new Models.MedicoModel());
                return Ok(token);
            }

            return BadRequest("usuário ou senha incorreta.");
        }
    }
}
