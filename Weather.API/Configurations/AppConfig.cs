using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Weather.API.Configurations
{
    public static class AppConfig
    {
        public static IServiceCollection GetMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
