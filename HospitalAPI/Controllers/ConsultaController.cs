using HospitalAPI.Models;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaRepositorios _consultaRepositorios;
        private readonly IPacienteRepositorios _pacienteRepositorios;


        public ConsultaController(IConsultaRepositorios consultaRepositorios, IPacienteRepositorios pacienteRepositorios)
        {
            _consultaRepositorios = consultaRepositorios;
            _pacienteRepositorios = pacienteRepositorios;
        }

        [HttpGet]
        public async Task<ActionResult<List<ConsultaModel>>> BuscarTodasConsultas()
        {
            List<ConsultaModel> consultas = await _consultaRepositorios.BuscarTodasConsultas();

            return Ok(consultas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultaModel>> BuscarConsultaPorId(Guid id)
        {
            ConsultaModel consulta = await _consultaRepositorios.BuscarConsultaPorId(id);
            

            return Ok(consulta);
        }

        [HttpPost]
        public async Task<ActionResult<ConsultaModel>> Cadastrar([FromBody] ConsultaModel consultaModel, int id)
        {
            var paciente = await _pacienteRepositorios.BuscarPacientePorId(id);
            decimal valor = paciente.TemConvenio ? 0 : 100;
            consultaModel.Valor = valor;
            consultaModel.DataDoCadastro = DateTime.Now;
            ConsultaModel consulta = await _consultaRepositorios.Cadastrar(consultaModel);

            return Ok(consulta);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ConsultaModel>> Atualizar([FromBody] ConsultaModel consultaModel, Guid id)
        {
            consultaModel.ConsultaId = id;
            ConsultaModel consulta = await _consultaRepositorios.Atualizar(consultaModel, id);

            return Ok(consulta);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ConsultaModel>> Apagar([FromBody] Guid id)
        {
            bool apagado = await _consultaRepositorios.Apagar(id);
            return Ok(apagado);
        }
    }
}
