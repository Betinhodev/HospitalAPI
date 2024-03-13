using HospitalAPI.Models;

namespace HospitalAPI.Repositorios.Interfaces
{
    public interface IRetornoRepositorios
    {
        Task<List<RetornoModel>> BuscarTodosRetornos();

        Task<RetornoModel> BuscarRetornoPorId(int id);

        Task<RetornoModel> Cadastrar(RetornoModel retorno, Guid id);

        Task<RetornoModel> Atualizar(RetornoModel retorno, int id);

        Task<bool> Apagar(int id);
    }
}
