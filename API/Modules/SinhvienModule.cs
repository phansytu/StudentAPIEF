
using StudentAPIw6.Services;
using StudentAPIw6.API.Validators.InputValidators;
using StudentAPIw6.API.Validators.BusinessValidators;

namespace StudentAPIw6.API.Module
{
    public static class SinhVienModule
    {
        public static IServiceCollection AddSinhVienModule(this IServiceCollection services)
        {
            // Repository
            services.AddScoped<ISinhVienRepository, SinhVienRepository>();

            // Service
            services.AddScoped<ISinhVienService, SinhVienService>();

            // Validators
            services.AddScoped<SinhVienValidator>();
            services.AddScoped<SinhVienBusinessValidator>();

            return services;
        }
    }
}