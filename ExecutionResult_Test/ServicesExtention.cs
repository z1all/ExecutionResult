using ExecutionResult_Test.DbContexts;
using ExecutionResult_Test.Interfaces;
using ExecutionResult_Test.Services;

namespace ExecutionResult_Test
{
    public static class ServicesExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<DbContext>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
