using HospitalAPI.Data;
using HospitalAPI.Models;
using HospitalAPI.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace HospitalAPI.Repositorios
{
    public class PacienteRepositorios : IPacienteRepositorios
    {
        private readonly HospitalAPIContext _context;
        
        public PacienteRepositorios(HospitalAPIContext hospitalAPIContext) 
        {
            _context = hospitalAPIContext;
        }
        public async Task<List<PacienteModel>> BuscarTodosPacientes()
        {
            return await _context.Pacientes.Include(x => x.Consulta).ToListAsync();
        }
        public async Task<PacienteModel> BuscarPacientePorId(int id)
        {
            return await _context.Pacientes.Include(x => x.Consulta).FirstOrDefaultAsync(x => x.PacienteId == id);
        }

        public async Task<PacienteModel> Cadastrar(PacienteModel paciente)
        {

            await _context.Pacientes.AddAsync(paciente);
            await _context.SaveChangesAsync();

            return paciente;
        }

        public async Task<PacienteModel> BuscarDocPorId(int id)
        {
            return await _context.Pacientes.FirstOrDefaultAsync(x => x.PacienteId == id);
        }

        public async Task<PacienteModel> Atualizar(PacienteModel paciente, int id)
        {
            PacienteModel pacientePorId = await BuscarPacientePorId(id);

            if (pacientePorId == null)
            {
                throw new Exception($"Paciente para o ID: {id} não foi encontrado no banco de dados.");
            }

            pacientePorId.Nome = paciente.Nome;
            pacientePorId.CPF = paciente.CPF;
            pacientePorId.Endereco = paciente.Endereco;
            pacientePorId.DataDeNascimento = paciente.DataDeNascimento;
            pacientePorId.ImgDocumento = paciente.ImgDocumento;

            _context.Pacientes.Update(pacientePorId);
            await _context.SaveChangesAsync();

            return pacientePorId;

        }

        public async Task<bool> Apagar(int id)
        {
            PacienteModel pacientePorId = await BuscarPacientePorId(id); 

            if(pacientePorId == null)
            {
                throw new Exception($"Paciente para o ID: {id} não foi encontrado no banco de dados.");
            }

            _context.Pacientes.Remove(pacientePorId);
            await _context.SaveChangesAsync();

            return true;
        }

    }

}

