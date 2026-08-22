using StudentAPIw6.Repository;
using StudentAPIw6.Services;
using StudentAPIw6.API.Validators.InputValidators;
using StudentAPIw6.API.Validators.BusinessValidators;

namespace StudentAPIw6.API.Module
{
    public static class LopHocModule
    {
        public static IServiceCollection AddLopHocModule(this IServiceCollection services)
        {
            // Repository
            services.AddScoped<ILopHocRepository, LopHocRepository>();

            // Service
            services.AddScoped<ILopHocService, LopHocService>();

            // Validators
            services.AddScoped<LopHocValidator>();
            services.AddScoped<LopHocBusinessValidator>();

            return services;
        }
    }
}