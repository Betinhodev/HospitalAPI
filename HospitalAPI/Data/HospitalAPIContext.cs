using HospitalAPI.Data.Map;
using HospitalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Data
{
    public class HospitalAPIContext : DbContext
    {
        public HospitalAPIContext(DbContextOptions<HospitalAPIContext> options) : base(options) { }

        public DbSet<PacienteModel> Pacientes { get; set;}
        public DbSet<MedicoModel> Medicos { get; set;}
        public DbSet<ConsultaModel> Consultas { get; set;}
        public DbSet<ConvenioModel> Convenios { get; set; }
        public DbSet<RetornoModel> Retornos {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PacienteMap());
            modelBuilder.ApplyConfiguration(new MedicoMap());
            modelBuilder.ApplyConfiguration(new ConsultaMap());
            modelBuilder.ApplyConfiguration(new ConvenioMap());
            modelBuilder.ApplyConfiguration(new RetornoMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
