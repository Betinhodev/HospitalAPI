using HospitalAPI.DTOs;
using HospitalAPI.Models;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        [HttpGet("GerarPdf")]
        public async Task<ActionResult<ConsultaModel>> GerarPdf([FromQuery] Guid id)
        {
            ConsultaModel consulta = await _consultaRepositorios.BuscarConsultaPorId(id);
            PacienteModel paciente = await _pacienteRepositorios.BuscarPacientePorId(consulta.PacienteId);

            string HtmlContent = @"<!DOCTYPE html>
            <html>
            <head>
            <title>Fatura Hospitalar</title>
            <style>
            body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 20px;
            }

            h1, h2, h3 {
            margin: 0;
            padding: 0;
            }

            .container {
            max-width: 800px;
            margin: 0 auto;
            background-color: #f4f4f4;
            padding: 20px;
            border: 1px solid #ccc;
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
            border: 1px solid #ccc;
            padding: 8px;
            text-align: left;
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
            }
            
                
            HtmlContent += @"<p><strong>Idade:</strong> 40 anos</p>
            <p><strong>Gênero:</strong> Masculino</p>
            <div>
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
    }
}
