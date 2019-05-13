using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityManagement.Repositories.Contexts;
using UniversityManagement.Repositories.Implements;

namespace UniversityManagement.Repositories.Mappings
{
    public class RepositoryMapping
    {
        public static void InitMap(IServiceCollection services)
        {
            services.AddSingleton(typeof(DbContextOptions<>), typeof(DbContextOptions<>));
            services.AddSingleton<DbContext, WebContext>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
