using HospitalAPI.Data;
using HospitalAPI.DTOs;
using HospitalAPI.Models;
using HospitalAPI.Repositorios.Interfaces;
using HospitalAPI.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.Repositorios
{
    public class MedicoRepositorios : IMedicoRepositorios
    {
        private readonly HospitalAPIContext _context;
        private readonly ILogger<MedicoRepositorios> _logger;
        PassHasher<MedicoModel> hashedPass = new PassHasher<MedicoModel>();

        public MedicoRepositorios(HospitalAPIContext hospitalAPIContext, ILogger<MedicoRepositorios> logger)
        {
            _context = hospitalAPIContext;
            _logger = logger;
        }
        public async Task<List<MedicoModel>> BuscarTodosMedicos()
        {
            return await _context.Medicos.ToListAsync();
        }
        public async Task<MedicoModel> BuscarMedicoPorId(int id)
        {
            return await _context.Medicos.FirstOrDefaultAsync(x => x.MedicoId == id);
        }

        public async Task<MedicoModel> Cadastrar(MedicoRequestDto request)
        {
            var medicoExistente = await _context.Medicos.FirstOrDefaultAsync(x => x.CPF == request.CPF);

            if(medicoExistente != null)
            {
                _logger.LogWarning("CPF já cadastrado.");
                throw new Exception("CPF já cadastrado.");
            }

            MedicoModel medico = new()
            {
                CPF = request.CPF,
                Nome = request.Nome,
                Password = request.Password
            };

            var senhaMedico = hashedPass.HashPassword(medico, request.Password);
            medico.Password = senhaMedico;

            await _context.Medicos.AddAsync(medico);
            await _context.SaveChangesAsync();

            return medico;
        }

        public async Task<MedicoModel> BuscarDocPorId(int id)
        {
            return await _context.Medicos.FirstOrDefaultAsync(x => x.MedicoId == id);
        }

        public async Task<MedicoModel> Atualizar(MedicoRequestDto medico, int id)
        {
            MedicoModel medicoPorId = await BuscarMedicoPorId(id);

            if(medicoPorId == null)
            {
                _logger.LogWarning($"O Medico para o ID: {id} não foi encontrado no banco de dados.");
                throw new Exception($"O Medico com ID: {id} não foi localizado no banco de dados.");
            }
            if (medico.imgDoc == null || medico.imgDoc.Length == 0)
            {
                throw new Exception("Nenhuma foto de documento foi carregada");
            }

            Guid guidDocConvenio = Guid.NewGuid();
            var imgPath = Path.Combine("Imagens/", $"{guidDocConvenio}");

            using (FileStream stream = System.IO.File.Create(imgPath))
            {
                await medico.imgDoc.CopyToAsync(stream);
            }

            medicoPorId.ImgDocumento = imgPath;
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
                _logger.LogWarning($"O Medico para o ID: {id} não foi encontrado no banco de dados.");
                throw new Exception($"O Medico com ID: {id} não foi localizado no banco de dados.");
            }

            _context.Medicos.Remove(medicoPorId);
            await _context.SaveChangesAsync();

            return true;
        }



    }
}
