using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Turnos> Turnos { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Medicos> Medicos { get; set; }
        public DbSet<Pacientes> Pacientes { get; set; }
        public DbSet<Documentos> Documentos { get; set; }
        public DbSet<Especialidades> Especialidades { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Estudios> Estudios { get; set; }
    }
}
