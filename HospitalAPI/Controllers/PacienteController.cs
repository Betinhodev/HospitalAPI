using HospitalAPI.Models;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteRepositorios _pacienteRepositorios;

        public PacienteController(IPacienteRepositorios pacienteRepositorios)
        {
            _pacienteRepositorios = pacienteRepositorios;
        }

        [HttpGet]
        public async Task<ActionResult<List<PacienteModel>>> BuscarTodosPacientes()
        {
            List<PacienteModel> paciente = await _pacienteRepositorios.BuscarTodosPacientes();

            return Ok(paciente);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteModel>> BuscarPacientePorId(int id)
        {
            PacienteModel paciente = await _pacienteRepositorios.BuscarPacientePorId(id);

            return Ok(paciente);
        }

        [HttpPost]
        public async Task<ActionResult<PacienteModel>> Cadastrar([FromBody] PacienteModel pacienteModel)
        {
            PacienteModel paciente = await _pacienteRepositorios.Cadastrar(pacienteModel);

            return Ok(paciente);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PacienteModel>> Atualizar([FromBody] PacienteModel pacienteModel, int id)
        {
            pacienteModel.PacienteId = id;
            PacienteModel paciente = await _pacienteRepositorios.Atualizar(pacienteModel, id);

            return Ok(paciente);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PacienteModel>> Apagar([FromBody] int id)
        {
            bool apagado = await _pacienteRepositorios.Apagar(id);
            return Ok(apagado);
        }
    }
}
