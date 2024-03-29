﻿using HospitalAPI.Models;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoRepositorios _medicoRepositorios;

        public MedicoController(IMedicoRepositorios medicoRepositorios)
        {
            _medicoRepositorios = medicoRepositorios;
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<List<MedicoModel>>> BuscarTodosMedicos()
        {
            List<MedicoModel> medicos = await _medicoRepositorios.BuscarTodosMedicos();

            return Ok(medicos);
        }
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoModel>> BuscarMedicoPorId(int id)
        {
            MedicoModel medico = await _medicoRepositorios.BuscarMedicoPorId(id);

            return Ok(medico);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<MedicoModel>> Cadastrar([FromBody] MedicoModel medicoModel)
        {
            MedicoModel medico = await _medicoRepositorios.Cadastrar(medicoModel);

            return Ok(medico);
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MedicoModel>> Atualizar([FromBody] MedicoModel medicoModel, int id)
        {
            medicoModel.MedicoId = id;
            MedicoModel medico = await _medicoRepositorios.Atualizar(medicoModel, id);

            return Ok(medico);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicoModel>> Apagar([FromBody] int id)
        {
            bool apagado = await _medicoRepositorios.Apagar(id);
            return Ok(apagado);
        }
    }
}
