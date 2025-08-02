using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITurnosRepository
    {
        Task<Turnos> AddAsync(Turnos turno);
        Task<bool> DeleteAsync(Turnos turno);
        Task<IEnumerable<Turnos>> GetAllAsync();
        Task<Turnos?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Turnos turno);
    }
}