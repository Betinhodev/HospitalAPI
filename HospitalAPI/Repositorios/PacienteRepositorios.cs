﻿using HospitalAPI.Data;
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
        private readonly ILogger<PacienteRepositorios> _logger;

        public PacienteRepositorios(HospitalAPIContext hospitalAPIContext, ILogger<PacienteRepositorios> logger) 
        {
            _context = hospitalAPIContext;
            _logger = logger;
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
            
            var pacienteExistente = _context.Pacientes.FirstOrDefaultAsync(x => x.CPF == paciente.CPF);

            if(pacienteExistente != null) 
            {
                _logger.LogWarning("CPF já cadastrado no banco de dados.");
                throw new Exception("CPF já cadastrado no banco de dados.");
            }

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
                _logger.LogWarning($"Paciente para o ID: {id} não foi encontrado no banco de dados.");
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
                _logger.LogWarning($"Paciente para o ID: {id} não foi encontrado no banco de dados.");
                throw new Exception($"Paciente para o ID: {id} não foi encontrado no banco de dados.");
            }

            _context.Pacientes.Remove(pacientePorId);
            await _context.SaveChangesAsync();

            return true;
        }

    }

}

