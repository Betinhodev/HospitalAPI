using HospitalAPI.Models;
using HospitalAPI.Repositorios;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetornoController : ControllerBase
    {
        private readonly IRetornoRepositorios _retornoRepositorios;
        private readonly IConsultaRepositorios _consultaRepositorios;


        public RetornoController(IRetornoRepositorios retornoRepositorios, IConsultaRepositorios consultaRepositorios)
        {
            _retornoRepositorios = retornoRepositorios;
            _consultaRepositorios = consultaRepositorios;
        }
        [Authorize(Roles = "medico, admin")]
        [HttpGet]
        public async Task<ActionResult<List<RetornoModel>>> BuscarTodosRepositorios()
        {
            List<RetornoModel> retornos = await _retornoRepositorios.BuscarTodosRetornos();

            return Ok(retornos);
        }
        [Authorize(Roles = "medico, admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RetornoModel>> BuscarRetornoPorId(int id)
        {
            RetornoModel retorno = await _retornoRepositorios.BuscarRetornoPorId(id);


            return Ok(retorno);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<RetornoModel>> Cadastrar([FromBody] RetornoModel retornoModel, Guid id)
        {
            var consulta = await _consultaRepositorios.BuscarConsultaPorId(id);

            retornoModel.Data = DateTime.Now;
            RetornoModel retorno = await _retornoRepositorios.Cadastrar(retornoModel, id);

            return Ok(retorno);
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<RetornoModel>> Atualizar([FromBody] RetornoModel retornoModel, int id)
        {
            retornoModel.RetornoId = id;
            RetornoModel consulta = await _retornoRepositorios.Atualizar(retornoModel, id);

            return Ok(consulta);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<RetornoModel>> Apagar([FromBody] int id)
        {
            bool apagado = await _retornoRepositorios.Apagar(id);
            return Ok(apagado);
        }
    }
}
