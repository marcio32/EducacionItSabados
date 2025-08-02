using Domain.Entities;

namespace Application.Repositorys
{
    public interface IEstudiosRepository
    {
        Task<IEnumerable<Estudios>> GetAllAsync();
        Task<Estudios?> GetByIdAsync(int id);
    }
}