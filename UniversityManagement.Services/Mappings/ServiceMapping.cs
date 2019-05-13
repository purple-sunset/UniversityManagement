using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityManagement.Services.Implements;

namespace UniversityManagement.Services.Mappings
{
    public class ServiceMapping
    {
        public static void InitMap(IServiceCollection services)
        {
            services.AddSingleton(typeof(IEntityBaseService<>), typeof(EntityBaseService<>));
            services.AddSingleton(typeof(IBaseService<,>), typeof(BaseService<,>));
            services.AddSingleton<IAccessTokenService, AccessTokenService>();
        }
    }
}
