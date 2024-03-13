using HospitalAPI.Models;
using HospitalAPI.Repositorios;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvenioController : ControllerBase
    {
        private readonly IConvenioRepositorios _convenioRepositorios;

        public ConvenioController(IConvenioRepositorios convenioRepositorios)
        {
            _convenioRepositorios = convenioRepositorios;
        }

        [HttpGet]
        public async Task<ActionResult<List<ConvenioModel>>> BuscarTodosConvenios()
        {
            List<ConvenioModel> convenios = await _convenioRepositorios.BuscarTodosConvenios();

            return Ok(convenios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConvenioModel>> BuscarConvenioPorId(int id)
        {
            ConvenioModel convenio = await _convenioRepositorios.BuscarConvenioPorId(id);

            return Ok(convenio);
        }

        [HttpPost]
        public async Task<ActionResult<ConvenioModel>> Cadastrar([FromBody] ConvenioModel convenioModel)
        {
            ConvenioModel convenio = await _convenioRepositorios.Cadastrar(convenioModel);

            return Ok(convenio);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ConvenioModel>> Atualizar([FromBody] ConvenioModel convenioModel, int id)
        {
            convenioModel.ConvenioId = id;
            ConvenioModel convenio = await _convenioRepositorios.Atualizar(convenioModel, id);

            return Ok(convenio);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ConvenioModel>> Apagar([FromBody] int id)
        {
            bool apagado = await _convenioRepositorios.Apagar(id);
            return Ok(apagado);
        }


    }
}
