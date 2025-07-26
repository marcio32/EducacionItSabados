using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IDocumentosRepository
    {
        Task<Documentos?> GetByIdAsync(int id);
        Task<IEnumerable<Documentos?>> GetByTurnoIdAsync(int turnoId);
    }
}