﻿using HospitalAPI.Models;

namespace HospitalAPI.Repositorios.Interfaces
{
    public interface IPacienteRepositorios
    {
        Task<List<PacienteModel>> BuscarTodosPacientes();

        Task<PacienteModel> BuscarPacientePorId(int id);

        Task<PacienteModel> BuscarDocPorId(int id);

        Task<PacienteModel> Cadastrar(PacienteModel paciente);

        Task<PacienteModel> Atualizar(PacienteModel paciente, int id);

        Task<bool> Apagar(int id);




    }
}
