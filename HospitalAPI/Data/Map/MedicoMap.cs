using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Data.Map
{
    public class MedicoMap : IEntityTypeConfiguration<MedicoModel>
    {
        public void Configure(EntityTypeBuilder<MedicoModel> builder)
        {
            builder.HasKey(x => x.MedicoId);
            builder.Property(x => x.CPF).IsRequired().HasMaxLength(15);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ConsultaId);
            builder.HasMany(x => x.Consulta);
            builder.HasMany(x => x.Laudo);
            builder.HasMany(x => x.Exame);
            builder.HasMany(x => x.Retorno);
        }

    }
}
