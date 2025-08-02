using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorys
{
    public class DocumentosRepository : IDocumentosRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public DocumentosRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Documentos?>> GetByTurnoIdAsync(int turnoId) => await _dbContext.Documentos.Where(x => x.TurnosId == turnoId).ToListAsync();
        public async Task<Documentos?> GetByIdAsync(int id) => await _dbContext.Documentos.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Documentos> CreateAsync(Documentos documento)
        {
            _dbContext.Documentos.Add(documento);
            await _dbContext.SaveChangesAsync();
            return documento;
        }
    }
}
