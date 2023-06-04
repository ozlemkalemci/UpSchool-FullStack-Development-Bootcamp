using Application.Common.Interfaces;
using Application.Utilities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Services;
using Infrastructure.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("MariaDB")!;

            services.AddScoped<Crawler>();

			services.AddScoped<IExcelService, ExcelManager>();

			services.AddScoped<IEmailService, EmailManager>();

            services.AddScoped<ISignalRClient, SignalRClient>();

            // DbContext
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            return services;
        }
    }
}
