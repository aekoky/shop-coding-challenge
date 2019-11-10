using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Newtonsoft.Json;
using ShopChallenge.Pagination;
using ShopChallenge.Repositories.Models;
using ShopChallenge.Services.Models;
using ShopChallenge.ServicesInjectors;
using System.Text;

namespace ShopChallenge
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            
            services.Configure<PasswordHasherOptions>(o => o.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2);

            // Getting The Settings 
            services.Configure<AppSettings>(settings => Configuration.GetSection("AppSettings").Bind(settings));
            AppSettings appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            
            services.AddSingleton(appSettings);

            // Configuring and injecting JWT
            services.InjectJWT(appSettings);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.InjectSwagger();

            //Configuring and Injectings AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ModelsProfiler());
                mc.AddProfile(new PageProfiler());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Injectings Repositories and Services
            services.InjectRepositories();
            services.InjectServices();

            //Configuring Database client
            services.AddSingleton(_ => new MongoClient(appSettings.ConnectionString));
            services.AddDbContext<ShopContext>(ServiceLifetime.Transient);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
                app.UseExceptionHandler(t => t.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;
                    var result = JsonConvert.SerializeObject(new { error = exception.Message });
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result).ConfigureAwait(false);
                }));
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            // Confugiring Security
            app.ConfigureJWT();

            // Confugiring Swagger
            app.ConfigureSwagger();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.DefaultPage = "/";
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
