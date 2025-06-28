using Domain.Entities;

namespace Infrastructure.Application
{
    public interface IRolesRepository
    {
        Task<bool> AddAsync(Roles rol);
        Task<bool> DeleteAsync(Roles rol);
        Task<List<Roles>> GetAllAsync();
        Task<List<Roles>> GetRolesActiveAsync();
        Task<Roles?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Roles rol);
    }
}