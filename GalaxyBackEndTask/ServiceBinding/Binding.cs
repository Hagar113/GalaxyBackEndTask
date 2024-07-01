using DataProvider.IProvider;
using DataProvider.Provider;
using Microsoft.Extensions.DependencyInjection;

namespace GalaxyBackEndTask.ServiceBinding
{
    public static class Binding
    {
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthProvider, AuthProvider>();
           

            return services;
        }
    }
}
