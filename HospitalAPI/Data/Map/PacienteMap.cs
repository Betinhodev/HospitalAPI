﻿using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalAPI.Data.Map
{
    public class PacienteMap : IEntityTypeConfiguration<PacienteModel>
    {
        public void Configure(EntityTypeBuilder<PacienteModel> builder)
        {
            builder.HasKey(x => x.PacienteId);
            builder.Property(x => x.CPF).IsRequired().HasMaxLength(15);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.DataDeNascimento).IsRequired();
            builder.Property(x => x.Endereco).IsRequired();
            builder.Property(x => x.ImgDocumento);
            builder.Property(x => x.TemConvenio);
            builder.Property(x => x.ConvenioId);
            builder.Property(x => x.ConsultaId);
            builder.HasMany(x => x.Consulta);
            builder.HasMany(x => x.Laudo);
            builder.HasMany(x => x.Exame);
            builder.HasMany(x => x.Retorno);
        }

    }
}
