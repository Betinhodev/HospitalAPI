using HospitalAPI.Data;
using HospitalAPI.Models;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositorios
{
    public class ConvenioRepositorios : IConvenioRepositorios
    {
        private readonly HospitalAPIContext _context;

        public ConvenioRepositorios(HospitalAPIContext hospitalAPIContext)
        {
            _context = hospitalAPIContext;
        }
        public async Task<List<ConvenioModel>> BuscarTodosConvenios()
        {
            return await _context.Convenios.ToListAsync();
        }
        public async Task<ConvenioModel> BuscarConvenioPorId(int id)
        {
            return await _context.Convenios.FirstOrDefaultAsync(x => x.ConvenioId == id);
        }


        public async Task<ConvenioModel> Cadastrar(ConvenioModel convenio)
        {
            await _context.Convenios.AddAsync(convenio);
            await _context.SaveChangesAsync();

            return convenio;
        }
        public async Task<ConvenioModel> Atualizar(ConvenioModel convenio, int id)
        {
            ConvenioModel convenioPorId = await BuscarConvenioPorId(id);

            if (convenioPorId == null)
            {
                throw new Exception($"O Convenio com ID: {id} não foi localizado no banco de dados.");
            }

            convenioPorId.Nome = convenio.Nome;

            _context.Convenios.Update(convenioPorId);
            await _context.SaveChangesAsync();

            return convenioPorId;
        }


        public async Task<bool> Apagar(int id)
        {
            ConvenioModel convenioPorId = await BuscarConvenioPorId(id);

            if (convenioPorId == null)
            {
                throw new Exception($"O Convenio com ID: {id} não foi localizado no banco de dados.");
            }

            _context.Convenios.Remove(convenioPorId);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
