using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorys
{
    public class TurnosRepository : ITurnosRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TurnosRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Turnos>> GetAllAsync() => await _dbContext.Turnos.Include(x => x.Medico).Include(x=> x.Paciente).Include(x=>x.Usuario).Include(x=> x.Estado).Include(x=> x.Documentos).ToListAsync();
        public async Task<Turnos?> GetByIdAsync(int id) => await _dbContext.Turnos.Include(x => x.Medico).Include(x => x.Paciente).Include(x => x.Usuario).Include(x => x.Documentos).FirstOrDefaultAsync(x => x.Id == id);
        public async Task<bool> UpdateAsync(Turnos turno)
        {
            _dbContext.Update(turno);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Turnos turno)
        {
            _dbContext.Update(turno);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Turnos> AddAsync(Turnos turno)
        {
            _dbContext.Add(turno);
            await _dbContext.SaveChangesAsync();
            return turno;
        }
    }
}
