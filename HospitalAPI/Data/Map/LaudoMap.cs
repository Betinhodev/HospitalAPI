using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalAPI.Data.Map
{
    public class LaudoMap : IEntityTypeConfiguration<LaudoModel>
    {
        public void Configure(EntityTypeBuilder<LaudoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Descrição).HasMaxLength(255);
            builder.Property(x => x.DataCriacao).HasMaxLength(255);
            builder.Property(x => x.MedicoId);
            builder.Property(x => x.PacienteId);
            builder.Property(x => x.ConsultaId);
            builder.HasOne(x => x.Medico);
            builder.HasOne(x => x.Paciente);

            
        }

    }
}
