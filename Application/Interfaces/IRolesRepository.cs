using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRolesRepository
    {
        Task<bool> AddAsync(Roles rol);
        Task<bool> DeleteAsync(Roles rol);
        Task<IEnumerable<Roles>> GetAllAsync();
        Task<IEnumerable<Roles>> GetRolesActiveAsync();
        Task<Roles?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Roles rol);
    }
}