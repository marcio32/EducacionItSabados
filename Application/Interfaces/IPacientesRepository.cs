using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPacientesRepository
    {
        Task<IEnumerable<Pacientes>> GetAllAsync();
        Task<Pacientes?> GetByIdAsync(int id);
        Task<Pacientes?> GetByDniAsync(int dni);
        Task<Pacientes> AddAsync(Pacientes pacientes);
    }
}