using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMedicosRepository
    {
        Task<IEnumerable<Medicos>> GetAllAsync();
        Task<Medicos?> GetByIdAsync(int id);
    }
}