using HospitalAPI.Data;
using HospitalAPI.Models;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositorios
{
    public class MedicoRepositorios : IMedicoRepositorios
    {
        private readonly HospitalAPIContext _context;

        public MedicoRepositorios(HospitalAPIContext hospitalAPIContext)
        {
            _context = hospitalAPIContext;
        }
        public async Task<List<MedicoModel>> BuscarTodosMedicos()
        {
            return await _context.Medicos.ToListAsync();
        }
        public async Task<MedicoModel> BuscarMedicoPorId(int id)
        {
            return await _context.Medicos.FirstOrDefaultAsync(x => x.MedicoId == id);
        }

        public async Task<MedicoModel> Cadastrar(MedicoModel medico)
        {
            await _context.Medicos.AddAsync(medico);
            await _context.SaveChangesAsync();

            return medico;
        }
        public async Task<MedicoModel> Atualizar(MedicoModel medico, int id)
        {
            MedicoModel medicoPorId = await BuscarMedicoPorId(id);

            if(medicoPorId == null)
            {
                throw new Exception($"O Medico com ID: {id} não foi localizado no banco de dados.");
            }

            medicoPorId.Nome = medico.Nome;

            _context.Medicos.Update(medicoPorId);
            await _context.SaveChangesAsync();

            return medicoPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            MedicoModel medicoPorId = await BuscarMedicoPorId(id);

            if (medicoPorId == null)
            {
                throw new Exception($"O Medico com ID: {id} não foi localizado no banco de dados.");
            }

            _context.Medicos.Remove(medicoPorId);
            await _context.SaveChangesAsync();

            return true;
        }



    }
}
