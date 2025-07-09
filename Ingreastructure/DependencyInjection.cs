using Application.Interfaces;
using Infrastructure.Application;
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
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
