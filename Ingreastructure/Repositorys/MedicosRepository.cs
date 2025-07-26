using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorys
{
    public class MedicosRepository : IMedicosRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MedicosRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Medicos>> GetAllAsync() => await _dbContext.Medicos.ToListAsync();

        public async Task<Medicos?> GetByIdAsync(int id) => await _dbContext.Medicos.FirstOrDefaultAsync(m => m.Id == id);
    }
}
