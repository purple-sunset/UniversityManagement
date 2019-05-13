using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniversityManagement.Repositories.Mappings;
using UniversityManagement.Services.Mappings;

namespace UniversityManagement.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Configure AutoMapper
            Mapper.Initialize(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Configure DI
            RepositoryMapping.InitMap(services);
            ServiceMapping.InitMap(services);

            // Configure identity server
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
                {
                    // Notice the schema name is case sensitive [ cookies != Cookies ]
                    options.DefaultScheme = "cookies";
                    options.DefaultChallengeScheme = "oidc";
                })

                .AddCookie("cookies", options => options.ForwardDefaultSelector = ctx => ctx.Request.Path.StartsWithSegments("/api") ? "jwt" : "cookies")
                .AddJwtBearer("jwt", options =>
                {
                    options.Authority = "https://localhost:44359";
                    options.Audience = "api1";
                    options.RequireHttpsMetadata = true;
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "cookies";
                    options.Authority = "https://localhost:44359";
                    options.RequireHttpsMetadata = true;
                    options.ClientId = "mvc";
                    options.SaveTokens = true;

                    options.ClientSecret = "secret";
                    options.ResponseType = "code id_token";
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.Scope.Add("api1");
                    options.Scope.Add("offline_access");

                    options.ForwardDefaultSelector = ctx => ctx.Request.Path.StartsWithSegments("/api") ? "jwt" : "oidc";
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            app.UseAuthentication();
            app.UseStaticFiles();
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
