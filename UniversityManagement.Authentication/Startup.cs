using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using UniversityManagement.Authentication.Mappings;
using UniversityManagement.Authentication.Models;
using UniversityManagement.Authentication.Stores;
using UniversityManagement.Repositories.Mappings;
using UniversityManagement.Services.Mappings;

namespace UniversityManagement.Authentication
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Configure AutoMapper
            Mapper.Initialize(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Configure DI
            RepositoryMapping.InitMap(services);
            ServiceMapping.InitMap(services);
            IdentityStoreMapping.InitMap(services);

            // Configure identity server
            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(Configuration.GetSection("IdentityServer:Resources"))
                .AddInMemoryApiResources(Configuration.GetSection("IdentityServer:Apis"))
                .AddInMemoryClients(Configuration.GetSection("IdentityServer:Clients"))
                .AddTestUsers(TestUsers.Users)
                .AddPersistedGrantStore<PersistedGrantStore>();

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
