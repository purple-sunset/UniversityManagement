using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityManagement.Authentication.Stores;

namespace UniversityManagement.Authentication.Mappings
{
    public class IdentityStoreMapping
    {
        public static void InitMap(IServiceCollection services)
        {
            services.AddSingleton<IPersistedGrantStore, PersistedGrantStore>();
        }
    }
}
