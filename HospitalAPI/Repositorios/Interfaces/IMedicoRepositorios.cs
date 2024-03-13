using HospitalAPI.Models;

namespace HospitalAPI.Repositorios.Interfaces
{
    public interface IMedicoRepositorios
    {
        Task<List<MedicoModel>> BuscarTodosMedicos();

        Task<MedicoModel> BuscarMedicoPorId(int id);

        Task<MedicoModel> Cadastrar(MedicoModel medico);

        Task<MedicoModel> Atualizar(MedicoModel medico, int id);

        Task<bool> Apagar(int id);
    }
}
