using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalAPI.Data.Map
{
    public class ExameMap : IEntityTypeConfiguration<ExameModel>
    {
        public void Configure(EntityTypeBuilder<ExameModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Descricao);
            builder.Property(x => x.Tipo);
            builder.Property(x => x.Status);
            builder.Property(x => x.DataAgendamento);
            builder.Property(x => x.MedicoResponsavelId);
            builder.Property(x => x.PacienteId);
            builder.Property(x => x.Valor);
            builder.HasOne(x => x.MedicoResponsavel);
            builder.HasOne(x => x.Paciente);




        }

    }
}
