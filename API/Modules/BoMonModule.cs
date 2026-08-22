using StudentAPIw6.Repository;
using StudentAPIw6.Services;
using StudentAPIw6.API.Validators.InputValidators;
using StudentAPIw6.API.Validators.BusinessValidators;

namespace StudentAPIw6.API.Module
{
    public static class BoMonModule
    {
        public static IServiceCollection AddBoMonModule(this IServiceCollection services)
        {
            // Repository
            services.AddScoped<IBoMonRepository, BoMonRepository>();

            // Service
            services.AddScoped<IBoMonService, BoMonService>();

            // Validators
            services.AddScoped<BoMonValidator>();
            services.AddScoped<BoMonBusinessValidator>();

            return services;
        }
    }
}