using HospitalAPI.Data;
using HospitalAPI.Models;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositorios
{
    public class RetornoRepositorios : IRetornoRepositorios
    {
        private readonly HospitalAPIContext _context;
        private readonly IConsultaRepositorios _consultaRepositorios;

        public RetornoRepositorios(HospitalAPIContext hospitalAPIContext, IConsultaRepositorios consultaRepositorios)
        {
            _context = hospitalAPIContext;
            _consultaRepositorios = consultaRepositorios;
        }
        public async Task<List<RetornoModel>> BuscarTodosRetornos()
        {
            return await _context.Retornos.ToListAsync();
        }
        public async Task<RetornoModel> BuscarRetornoPorId(int id)
        {
            return await _context.Retornos.FirstOrDefaultAsync(x => x.RetornoId == id);
        }

        public async Task<RetornoModel> Cadastrar(RetornoModel retorno, Guid id)
        {
            var consulta = await _consultaRepositorios.BuscarConsultaPorId(id);

            if (consulta == null)
            {
                throw new BadHttpRequestException("Consulta não localizada no banco de dados, verifique se a ID está correta.");
            }
            if (consulta.DataDoCadastro < DateTime.Now.AddDays(-30))
            {
                throw new BadHttpRequestException("A Consulta especificada foi cadastrada a 30 dias ou mais, neste caso é necessário a abertura de uma nova consulta.");
            }

            retorno.Data = DateTime.Now;
            await _context.Retornos.AddAsync(retorno);
            await _context.SaveChangesAsync();

            return retorno;
        }
        public async Task<RetornoModel> Atualizar(RetornoModel retorno, int id)
        {
            RetornoModel retornoPorId = await BuscarRetornoPorId(id);

            if (retornoPorId == null)
            {
                throw new Exception($"O Retorno com ID: {id} não foi localizada no banco de dados.");
            }

            retornoPorId.Data = retorno.Data;
            retornoPorId.MedicoId = retorno.MedicoId;

            _context.Retornos.Update(retornoPorId);
            await _context.SaveChangesAsync();

            return retornoPorId;
        }



        public async Task<bool> Apagar(int id)
        {
            RetornoModel retornoPorId = await BuscarRetornoPorId(id);

            if (retornoPorId == null)
            {
                throw new Exception($"O Retorno com ID: {id} não foi localizada no banco de dados.");
            }

            _context.Retornos.Remove(retornoPorId);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
