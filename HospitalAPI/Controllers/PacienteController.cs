using HospitalAPI.DTOs;
using HospitalAPI.Models;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteRepositorios _pacienteRepositorios;
        private readonly ILogger<PacienteController> _logger;

        public PacienteController(IPacienteRepositorios pacienteRepositorios, ILogger<PacienteController> logger)
        {
            _pacienteRepositorios = pacienteRepositorios;
            _logger = logger;
        }

        [Authorize(Roles = "medico, admin")]
        [HttpGet]
        [Route("All", Name = "BuscaTodosPacientes")]
        public async Task<ActionResult<List<PacienteModel>>> BuscarTodosPacientes()
        {
            _logger.LogInformation($"Realizada consulta de todos os pacientes.");

            List <PacienteModel> paciente = await _pacienteRepositorios.BuscarTodosPacientes();

            return Ok(paciente);
        }

        [Authorize(Roles = "medico ,admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteModel>> BuscarPacientePorId(int id)
        {
            _logger.LogInformation($"Realizada consulta de paciente - Id: {id}.");

            PacienteModel paciente = await _pacienteRepositorios.BuscarPacientePorId(id);

            return Ok(paciente);
        }
        [Authorize(Roles = "medico, admin")]
        [HttpPost]
        public async Task<ActionResult<PacienteModel>> Cadastrar([FromForm] PacienteRequestDto requestDto, PatientRegisterDto patientRegister)
        {


            if (requestDto.imgDoc == null || requestDto.imgDoc.Length == 0)
            {
                return BadRequest("Nenhuma foto de documento foi carregada");
            }



            Guid guidDocConvenio = Guid.NewGuid();
            var imgPath = Path.Combine("Imagens/", $"{guidDocConvenio}");

            using (FileStream stream = System.IO.File.Create(imgPath))
            {
                await requestDto.imgDoc.CopyToAsync(stream);
            }

            requestDto.ImgDocumento = imgPath;

            PacienteModel paciente = await _pacienteRepositorios.Cadastrar(requestDto, patientRegister);

            _logger.LogInformation(message: "Paciente cadastrado.");

            return Ok(paciente);
        }
        [Authorize(Roles = "medico,admin")]
        [HttpGet("MostrarDocumento")]
        public async Task<ActionResult<PacienteModel>> BuscarDocPorId(int id)
        {
            _logger.LogInformation($"Realizada busca de documento por Id: {id}");

            PacienteModel paciente = await _pacienteRepositorios.BuscarDocPorId(id);

            if (paciente.ImgDocumento == null)
            {
                _logger.LogInformation($"Paciente não possui foto da carteira do convênio");
                return BadRequest("Este paciente não possui foto da carteira do convênio");
            }

            var imgPath = paciente.ImgDocumento;

            if (imgPath is null)
            {
                _logger.LogInformation($"Paciente não possui carteira do convênio");
                return BadRequest("Este paciente não possui carteira do convênio");
            }

            Byte[] b = System.IO.File.ReadAllBytes($"{imgPath}");
            return File(b, "image/png");
        }
        [Authorize(Roles = "paciente, medico, admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<PacienteModel>> Atualizar([FromBody] PacienteModel pacienteModel, int id)
        {
            _logger.LogInformation($"Realizado update do paciente - {pacienteModel.Nome}");
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if(userIdClaim == null)
                return Unauthorized("Token de autenticação inválido.");

            int authenticatedUserId = int.Parse(userIdClaim.Value);

            if (id != authenticatedUserId)
            {
                _logger.LogWarning("Você não tem permissão para atualizar os dados de outro paciente.");
                return Forbid("Você não tem permissão para atualizar os dados de outro paciente.");
            }

            if (User.IsInRole("paciente"))
            {

                PacienteModel pacienteExistente = await _pacienteRepositorios.BuscarPacientePorId(id);
                if (pacienteExistente == null)
                {
                    _logger.LogWarning("Paciente não encontrado");
                    return NotFound("Paciente não encontrado"); 
                }

                pacienteExistente.Nome = pacienteModel.Nome;
                pacienteExistente.DataDeNascimento = pacienteModel.DataDeNascimento;
                pacienteExistente.Endereco = pacienteModel.Endereco;

                
            }

            pacienteModel.PacienteId = id;
            PacienteModel paciente = await _pacienteRepositorios.Atualizar(pacienteModel, id);

            return Ok(paciente);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PacienteModel>> Apagar([FromBody] int id)
        {
            _logger.LogInformation($"Realizada exclusão do paciente {id}");
            bool apagado = await _pacienteRepositorios.Apagar(id);
            return Ok(apagado);
        }



    }
}
