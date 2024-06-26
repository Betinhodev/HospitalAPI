﻿using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Models
{
    public class PacienteModel
    {
        [Key]
        public int PacienteId { get; set; }

        public string? CPF { get; set; }

        public string? Password {  get; set; }

        public string? Nome { get; set; }

        public string? Endereco { get; set; }

        public string? DataDeNascimento { get; set; }
        public string? ImgDocumento { get; set; }

        public bool TemConvenio { get; set; }

        public int? ConvenioId { get; set; }

        public int? ConsultaId {  get; set; }

        public ICollection<ConsultaModel> Consulta { get; set; } = [];

        public ICollection<LaudoModel> Laudo { get; set; } = [];

        public ICollection<ExameModel> Exame { get; set; } = [];

        public ICollection<RetornoModel> Retorno { get; set; } = [];


    }
}
