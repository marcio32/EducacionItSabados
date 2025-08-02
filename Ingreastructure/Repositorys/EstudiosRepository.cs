using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Repositorys;

namespace Infrastructure.Repositorys
{
    public class EstudiosRepository : IEstudiosRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EstudiosRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Estudios>> GetAllAsync() => await _dbContext.Estudios.ToListAsync();

        public async Task<Estudios?> GetByIdAsync(int id) => await _dbContext.Estudios.FirstOrDefaultAsync(m => m.Id == id);
    }
}
