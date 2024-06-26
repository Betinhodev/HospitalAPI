﻿using HospitalAPI.Enums;
using HospitalAPI.Models;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaRepositorios _consultaRepositorios;
        private readonly IPacienteRepositorios _pacienteRepositorios;
        private readonly ILogger<ConsultaController> _logger;

        public ConsultaController(IConsultaRepositorios consultaRepositorios, IPacienteRepositorios pacienteRepositorios, ILogger<ConsultaController> logger)
        {
            _consultaRepositorios = consultaRepositorios;
            _pacienteRepositorios = pacienteRepositorios;
            _logger = logger;   
        }

        [Authorize(Roles = "paciente, medico, admin")]
        [HttpGet]
        public async Task<ActionResult<List<ConsultaModel>>> BuscarTodasConsultas()
        {
            _logger.LogInformation("Realizada busca de todas as consultas cadastradas.");
            List<ConsultaModel> consultas = await _consultaRepositorios.BuscarTodasConsultas();

            return Ok(consultas);
        }
        [Authorize(Roles = "paciente, medico, admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultaModel>> BuscarConsultaPorId(Guid id)
        {
            _logger.LogInformation($"Realizada busca de consulta - Id: {id}");

            ConsultaModel consulta = await _consultaRepositorios.BuscarConsultaPorId(id);
            

            return Ok(consulta);
        }

        [Authorize(Roles = "medico, admin")]
        [HttpPost]
        public async Task<ActionResult<ConsultaModel>> Cadastrar([FromForm] ConsultaModel consultaModel, int id)
        {

            _logger.LogInformation("Cadastro de nova consulta realizado.");
            var paciente = await _pacienteRepositorios.BuscarPacientePorId(id);
            decimal valor = paciente.TemConvenio ? 0 : 100;
            consultaModel.Valor = valor;
            consultaModel.DataDoCadastro = DateTime.Now;
            ConsultaModel consulta = await _consultaRepositorios.Cadastrar(consultaModel);

            return Ok(consulta);
        }

        [Authorize(Roles = "medico, admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ConsultaModel>> Atualizar([FromBody] ConsultaModel consultaModel, Guid id)
        {
            consultaModel.ConsultaId = id;
            ConsultaModel consulta = await _consultaRepositorios.Atualizar(consultaModel, id);

            return Ok(consulta);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ConsultaModel>> Apagar([FromBody] Guid id)
        {
            _logger.LogInformation($"Realizada exclusão da consulta - Id: {id}");
            bool apagado = await _consultaRepositorios.Apagar(id);
            return Ok(apagado);
        }

        [Authorize(Roles = "medico, admin")]
        [HttpGet("GerarPdf")]
        public async Task<ActionResult<ConsultaModel>> GerarPdf([FromQuery] Guid id)
        {
            ConsultaModel consulta = await _consultaRepositorios.BuscarConsultaPorId(id);
            PacienteModel paciente = await _pacienteRepositorios.BuscarPacientePorId(consulta.PacienteId);
            _logger.LogInformation($"Gerado fatura da consulta: {consulta.ConsultaId}");

            string HtmlContent = @"<!DOCTYPE html>
            <html>
            <head>
            <title>Fatura Hospitalar</title>
            <style>
            body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 20px;
            background-color: #f0f6ff; /* Azul claro para o fundo */
            color: #333; /* Cor de texto principal */
            }

            .container {
            max-width: 800px;
            margin: 0 auto;
            background-color: #fff; /* Fundo branco para o contêiner */
            padding: 20px;
            border-radius: 8px; /* Cantos arredondados */
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); /* Sombra suave */
            }

            .header {
            text-align: center;
            margin-bottom: 20px;
            }

            .invoice-details {
            margin-bottom: 20px;
            }

            table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
            }

            th, td {
            border: 1px solid #ddd; /* Cinza claro para bordas da tabela */
            padding: 12px;
            text-align: left;
            }

            th {
            background-color: #f0f6ff; /* Azul claro para cabeçalho da tabela */
            }

            .footer {
            text-align: center;
            margin-top: 20px;
            color: #666;
            }
            </style>
            </head>
            <body>
            <div class='container'>
            <div class='header'>
            <h1>Fatura Hospitalar</h1>
            <p>Hospital API</p>
            </div>

            <div class='invoice-details'>
            <h2>Detalhes da Consulta</h2>";
            if (consulta != null)
            {
                HtmlContent += "<p><strong>ID da Consulta: </strong>"+consulta.ConsultaId+"</p>";
                HtmlContent += "<p><strong>Data da Consulta: </strong>"+consulta.DataDoCadastro+"</p>";
            }

            HtmlContent += @"</div>
            <div class='patient-details'>
            <h2>Detalhes do Paciente</h2>";
            if(paciente != null){

                HtmlContent += "<p><strong>Nome: </strong>"+paciente.Nome+"</p>";
                HtmlContent += "<p><strong>Data de Nascimento: </strong>"+paciente.DataDeNascimento+"</p>";
            }
            
                

            HtmlContent += @"<div>
            <div class='invoice-items'>
            <h2>Itens da Fatura</h2>
            <table>
            <tr>
            <th>Serviço</th>
            <th>Quantidade</th>
            <th>Total</th>
            </tr>";
            HtmlContent += @"<tr>
            <td>Consulta Médica</td>
            <td>1</td>";
            if(consulta != null)
            {
                HtmlContent += "<td>" + consulta.Valor + "</td>";
            }
            HtmlContent +="</ tr >";
            HtmlContent += @"
            </table>
            </div>
            <div class='footer'>
            <p>Esta é uma fatura gerada automaticamente.</p>
            </div>
            </div>
            </body>
            </html>";
            var document = new PdfDocument();
            PdfGenerator.AddPdfPages(document, HtmlContent, PageSize.A4);
            byte[]? response = null;
            using(MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }

            
            string fileName = "Fatura_" + "teste" + ".pdf";
            string contentType = "application/pdf";
            return File(response, contentType ,fileName);

        }

        [HttpGet("FiltrarConsultas")]
        public async Task<ActionResult<ConsultaModel>> FiltrarConsultas([FromForm] int pacienteId, StatusConsulta status)
        {

            //Busca paciente por Id
            List<ConsultaModel> consulta = await _consultaRepositorios.FiltrarConsultas(pacienteId, status);
            _logger.LogInformation($"Realizada busca das consultas do paciente - Id: {pacienteId} com Status: {status}");
            return Ok(consulta);
        }
    }
}
