using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Data.Map
{
    public class ConsultaMap : IEntityTypeConfiguration<ConsultaModel>
    {
        public void Configure(EntityTypeBuilder<ConsultaModel> builder)
        {
            builder.HasKey(x => x.ConsultaId);
            builder.Property(x => x.MedicoId).IsRequired();
            builder.Property(x => x.PacienteId).IsRequired();
            builder.Property(x => x.DataDoCadastro).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Valor);
            builder.HasOne(x => x.Paciente);
            builder.HasOne(x => x.Medico);


        }

    }
}
