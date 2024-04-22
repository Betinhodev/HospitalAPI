using HospitalAPI.DTOs;
using HospitalAPI.Models;
using HospitalAPI.Repositorios;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoRepositorios _medicoRepositorios;
        private readonly ILogger<MedicoController> _logger;

        public MedicoController(IMedicoRepositorios medicoRepositorios, ILogger<MedicoController> logger)
        {
            _medicoRepositorios = medicoRepositorios;
            _logger = logger;
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<List<MedicoModel>>> BuscarTodosMedicos()
        {
            _logger.LogInformation("Realizada consulta de todos os medicos.");
            List<MedicoModel> medicos = await _medicoRepositorios.BuscarTodosMedicos();

            return Ok(medicos);
        }
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoModel>> BuscarMedicoPorId(int id)
        {
            MedicoModel medico = await _medicoRepositorios.BuscarMedicoPorId(id);
            _logger.LogInformation($"Realizada consulta do médico: {medico.Nome}");
            return Ok(medico);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<MedicoModel>> Cadastrar([FromForm] MedicoRequestDto requestDto)
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

            MedicoModel medico = await _medicoRepositorios.Cadastrar(requestDto);
            _logger.LogInformation($"Realizado cadastro do médico: {medico.Nome}");
            return Ok(medico);
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MedicoModel>> Atualizar([FromForm] MedicoRequestDto requestDto, int id)
        {
            
            MedicoModel medico = await _medicoRepositorios.Atualizar(requestDto, id);
            _logger.LogInformation($"Realizado update do médico: {medico.Nome}");
            return Ok(medico);
        }

        [Authorize(Roles="medico, admin")]
        [HttpGet("DocumentoMedico")]
        public async Task<ActionResult<MedicoModel>> BuscarDocPorId(int id)
        {
            _logger.LogInformation($"Realizada busca de documento por Id: {id}");

            MedicoModel medico = await _medicoRepositorios.BuscarDocPorId(id);

            if (medico.ImgDocumento == null)
            {
                _logger.LogWarning($"Médico não possui foto da carteira do convênio");
                return BadRequest("Este médico não possui foto da carteira do convênio");
            }

            var imgPath = medico.ImgDocumento;

            if (imgPath is null)
            {
                _logger.LogInformation($"Paciente não possui carteira do convênio");
                return BadRequest("Este paciente não possui carteira do convênio");
            }

            Byte[] b = System.IO.File.ReadAllBytes($"{imgPath}");
            return File(b, "image/png");
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicoModel>> Apagar([FromBody] int id)
        {
            _logger.LogInformation($"Realizada exlcusão do médico - Id: {id}");
            bool apagado = await _medicoRepositorios.Apagar(id);
            return Ok(apagado);
        }
    }
}
