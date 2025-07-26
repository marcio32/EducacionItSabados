using Application.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositorys;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddScoped<ITurnosRepository, TurnosRepository>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMedicosRepository, MedicosRepository>();
            services.AddScoped<IPacientesRepository, PacientesRepository>();
            services.AddScoped<IDocumentosRepository, DocumentosRepository>();
            return services;
        }
    }
}
