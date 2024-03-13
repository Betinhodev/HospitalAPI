using HospitalAPI.Models;

namespace HospitalAPI.Repositorios.Interfaces
{
    public interface IConvenioRepositorios
    {
        Task<List<ConvenioModel>> BuscarTodosConvenios();

        Task<ConvenioModel> BuscarConvenioPorId(int id);

        Task<ConvenioModel> Cadastrar(ConvenioModel convenio);

        Task<ConvenioModel> Atualizar(ConvenioModel convenio, int id);

        Task<bool> Apagar(int id);
    }
}
