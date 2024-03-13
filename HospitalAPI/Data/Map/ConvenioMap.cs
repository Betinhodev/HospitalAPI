using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalAPI.Data.Map
{
    public class ConvenioMap : IEntityTypeConfiguration<ConvenioModel>
    {
        public void Configure(EntityTypeBuilder<ConvenioModel> builder)
        {
            builder.HasKey(x => x.ConvenioId);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(50);
        }
    }
}
