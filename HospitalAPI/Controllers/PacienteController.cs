using HospitalAPI.DTOs;
using HospitalAPI.Models;
using HospitalAPI.Repositorios;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;

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
        [Route("All", Name = "BuscaTodosPacientes")]
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
        public async Task<ActionResult<PacienteModel>> Cadastrar([FromForm] PacienteRequestDto requestDto)
        {
            if (requestDto.imgDoc == null || requestDto.imgDoc.Length == 0)
            {
                return BadRequest("Nenhuma foto de documento foi carregada");
            }

            Guid guidDocConvenio = Guid.NewGuid();
            var imgPath = Path.Combine("Imagens/", $"{guidDocConvenio + requestDto.imgDoc.FileName}");

            using (FileStream stream = System.IO.File.Create(imgPath))
            {
                await requestDto.imgDoc.CopyToAsync(stream);
            }

            requestDto.ImgDocumento = imgPath;
            requestDto.PacienteId = 0;

            PacienteModel paciente = await _pacienteRepositorios.Cadastrar(requestDto);


            return Ok(paciente);
        }

        [HttpGet("MostrarDocumento")]
        public async Task<ActionResult<PacienteModel>> BuscarDocPorId(int id)
        {
            PacienteModel paciente = await _pacienteRepositorios.BuscarDocPorId(id);

            if (paciente.ImgDocumento == null)
            {
                return BadRequest("Este paciente não possui foto da carteira do convênio");
            }

            var imgPath = paciente.ImgDocumento;

            if (imgPath is null)
            {
                return BadRequest("Este paciente não possui carteira do convênio");
            }

            Byte[] b = System.IO.File.ReadAllBytes($"{imgPath}");
            return File(b, "image/png");
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
