using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorys
{
    public class PacientesRepository : IPacientesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PacientesRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Pacientes>> GetAllAsync() => await _dbContext.Pacientes.ToListAsync();
        public async Task<Pacientes?> GetByIdAsync(int id) => await _dbContext.Pacientes.FirstOrDefaultAsync(x => x.Id == id);

    }
}
