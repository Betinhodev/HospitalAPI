using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Data.Map
{
    public class RetornoMap : IEntityTypeConfiguration<RetornoModel>
    {
        public void Configure(EntityTypeBuilder<RetornoModel> builder)
        {
            builder.HasKey(x => x.RetornoId);
            builder.Property(x => x.MedicoId).IsRequired();
            builder.Property(x => x.PacienteId).IsRequired();
            builder.Property(x => x.ConsultaId).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }

    }
}
