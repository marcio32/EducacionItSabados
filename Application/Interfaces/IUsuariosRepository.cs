using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<bool> AddAsync(Usuarios usuario);
        Task<bool> DeleteAsync(Usuarios usuario);
        Task<IEnumerable<Usuarios>> GetAllAsync();
        Task<Usuarios?> GetByIdAsync(int id);
        Task<Usuarios?> GetByEmailAsync(string email);
        Task<bool> UpdateAsync(Usuarios usuario);
    }
}