using HospitalAPI.Data;
using HospitalAPI.Enums;
using HospitalAPI.Models;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositorios
{
    public class ConsultaRepositorios : IConsultaRepositorios
    {
        private readonly HospitalAPIContext _context;

        public ConsultaRepositorios(HospitalAPIContext hospitalAPIContext)
        {
            _context = hospitalAPIContext;
        }
        public async Task<List<ConsultaModel>> BuscarTodasConsultas()
        {
            return await _context.Consultas.ToListAsync();
        }
        public async Task<ConsultaModel> BuscarConsultaPorId(Guid id)
        {
            return await _context.Consultas.FirstOrDefaultAsync(x => x.ConsultaId == id);
        }

        public async Task<ConsultaModel> Cadastrar(ConsultaModel consulta)
        {

            await _context.Consultas.AddAsync(consulta);
            consulta.DataDoCadastro = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return consulta;
        }

        public async Task<ConsultaModel> Atualizar(ConsultaModel consulta, Guid id)
        {
            ConsultaModel consultaPorId = await BuscarConsultaPorId(id);

            if (consultaPorId == null)
            {
                throw new Exception($"A Consulta com ID: {id} não foi localizada no banco de dados.");
            }

            consultaPorId.DataDoCadastro = consulta.DataDoCadastro;
            consultaPorId.MedicoId = consulta.MedicoId;

            _context.Consultas.Update(consultaPorId);
            await _context.SaveChangesAsync();

            return consultaPorId;

        }


        public async Task<bool> Apagar(Guid id)
        {
            ConsultaModel consultaPorId = await BuscarConsultaPorId(id);

            if (consultaPorId == null)
            {
                throw new Exception($"A Consulta com ID: {id} não foi localizada no banco de dados.");
            }

            _context.Consultas.Remove(consultaPorId);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<ConsultaModel>> FiltrarConsultas(int pacienteId, StatusConsulta status)
        {
            return await _context.Consultas.Where(c => c.PacienteId == pacienteId).Where(s => s.Status == status).ToListAsync();

            
        }
    }
}
