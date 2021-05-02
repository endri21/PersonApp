using Microsoft.Extensions.DependencyInjection;
using PersonCore.Interfaces;
using PersonCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonCore.Configurations
{
   public static class DIBuilder
    {
        public static void ConfigureSettingsServices(IServiceCollection services)
        {
           
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
