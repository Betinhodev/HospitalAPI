using HospitalAPI.Models;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvenioController : ControllerBase
    {
        private readonly IConvenioRepositorios _convenioRepositorios;
        private readonly ILogger<ConvenioController> _logger;

        public ConvenioController(IConvenioRepositorios convenioRepositorios, ILogger<ConvenioController> logger)
        {
            _convenioRepositorios = convenioRepositorios;
            _logger = logger;
        }
        [Authorize(Roles = "paciente, medico, admin")]
        [HttpGet]
        public async Task<ActionResult<List<ConvenioModel>>> BuscarTodosConvenios()
        {

            _logger.LogInformation("Realizada consulta de todos os convenios.");
            List<ConvenioModel> convenios = await _convenioRepositorios.BuscarTodosConvenios();

            return Ok(convenios);

        }
        [Authorize(Roles = "paciente, medico, admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ConvenioModel>> BuscarConvenioPorId(int id)
        {
            ConvenioModel convenio = await _convenioRepositorios.BuscarConvenioPorId(id);
            _logger.LogInformation($"Realizada consulta do convenio: {convenio.Nome}");

            return Ok(convenio);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<ConvenioModel>> Cadastrar([FromBody] ConvenioModel convenioModel)
        {
            _logger.LogInformation("Relizado cadastro de novo convenio.");
            ConvenioModel convenio = await _convenioRepositorios.Cadastrar(convenioModel);

            return Ok(convenio);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ConvenioModel>> Atualizar([FromBody] ConvenioModel convenioModel, int id)
        {
            convenioModel.ConvenioId = id;
            ConvenioModel convenio = await _convenioRepositorios.Atualizar(convenioModel, id);
            _logger.LogInformation($"Realizado update do convenio: {convenio.Nome}");

            return Ok(convenio);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]

        public async Task<ActionResult<ConvenioModel>> Apagar([FromBody] int id)
        {
            _logger.LogInformation($"Realizada exclusão do convênio - Id: {id}");
            bool apagado = await _convenioRepositorios.Apagar(id);
            return Ok(apagado);
        }


    }
}
