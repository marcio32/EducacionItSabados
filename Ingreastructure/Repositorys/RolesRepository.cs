using Domain.Entities;
using Infrastructure.Application;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorys
{
    public class RolesRepository : IRolesRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public RolesRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<List<Roles>> GetAllAsync() => await _dbContext.Roles.ToListAsync();
        public async Task<List<Roles>> GetRolesActiveAsync() => await _dbContext.Roles.Where(x=> x.Estado == true).ToListAsync();

        public async Task<Roles?> GetByIdAsync(int id) => await _dbContext.Roles.FindAsync(id);

        public async Task<bool> AddAsync(Roles rol)
        {
            _dbContext.Add(rol);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Roles rol)
        {
            rol.Estado = false;
            _dbContext.Update(rol);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Roles rol)
        {
            _dbContext.Update(rol);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
