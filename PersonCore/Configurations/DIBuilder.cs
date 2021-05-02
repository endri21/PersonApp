using Microsoft.Extensions.DependencyInjection;
using PersonCore.Interfaces;
using PersonCore.Repositories;


namespace PersonCore.Configurations
{
   public static class DIBuilder
    {
        public static void ConfigureSettingsServices(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<ILoginProviderRepository, LoginProviderRepository>();
        }
    }
}
