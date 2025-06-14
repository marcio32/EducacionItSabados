using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositorys
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UsuariosRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Usuarios>> GetAllAsync() => await _dbContext.Usuarios.Include(x=> x.Rol).ToListAsync();
        public async Task<Usuarios?> GetByIdAsync(int id) => await _dbContext.Usuarios.FindAsync(id);
        public async Task<bool> UpdateAsync(Usuarios usuario)
        {
            _dbContext.Update(usuario);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Usuarios usuario)
        {
            usuario.Estado = false;
            _dbContext.Update(usuario);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddAsync(Usuarios usuario)
        {
            _dbContext.Add(usuario);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
