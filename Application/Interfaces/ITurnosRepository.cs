using Domain.Entities;

namespace Infrastructure.Repositorys
{
    public interface ITurnosRepository
    {
        Task<bool> AddAsync(Turnos turno);
        Task<bool> DeleteAsync(Turnos turno);
        Task<IEnumerable<Turnos>> GetAllAsync();
        Task<Turnos?> GetByEmailAsync(string email);
        Task<Turnos?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Turnos turno);
    }
}