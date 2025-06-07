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

    }

}
