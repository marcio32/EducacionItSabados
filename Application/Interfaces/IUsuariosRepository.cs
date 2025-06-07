using Domain.Entities;

namespace Infrastructure.Repositorys
{
    public interface IUsuariosRepository
    {
        Task<bool> AddAsync(Usuarios usuario);
        Task<bool> DeleteAsync(Usuarios usuario);
        Task<IEnumerable<Usuarios>> GetAllAsync();
        Task<Usuarios?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Usuarios usuario);
    }
}