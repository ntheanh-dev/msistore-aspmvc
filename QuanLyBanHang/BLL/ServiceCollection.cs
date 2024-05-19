using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddBLLServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceCollection).Assembly);

            return services;
        }
    }
}
