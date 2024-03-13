using HospitalAPI.Models;

namespace HospitalAPI.Repositorios.Interfaces
{
    public interface IConsultaRepositorios
    {
        Task<List<ConsultaModel>> BuscarTodasConsultas();

        Task<ConsultaModel> BuscarConsultaPorId(Guid id);

        Task<ConsultaModel> Cadastrar(ConsultaModel consulta);

        Task<ConsultaModel> Atualizar(ConsultaModel consulta, Guid id);

        Task<bool> Apagar(Guid id);
    }
}
