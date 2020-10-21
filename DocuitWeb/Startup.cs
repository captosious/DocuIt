using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DocuitWeb.Data;
using DocuitWeb.Models;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Blazored.Toast;
using ProtectedLocalStore;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

namespace DocuitWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredToast();
            services.AddProtectedLocalStore(new EncryptionService(
                new KeyInfo("45BLO2yoJkvBwz99kBEMlNkxvL40vUSGaqr/WBu3+Vg=", "Ou3fn+I9SVicGWMLkFEgZQ==")));
            

            services.AddSingleton<CompanyService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<SecurityService>();
            services.AddSingleton<StatusService>();
            services.AddSingleton<ProjectService>();
            services.AddSingleton<DossierService>();
            services.AddSingleton<DossierElementService>();
            services.AddSingleton<WorkingCenterService>();
            services.AddSingleton<BuildingTypeService>();
            services.AddSingleton<AccessService>();

            services.AddSingleton<AppSettings>();
            services.AddScoped<MyBlocker>();

            services.AddBlazorise(options =>
              {
                  options.ChangeTextOnKeyPress = false; // optional
              })
              .AddBootstrapProviders()
              .AddFontAwesomeIcons();
                
            // Add LocalLanguages.
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            //services.AddBootstrapCss();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.ApplicationServices.UseBootstrapProviders();

            // Globalization Init
            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-GB"),
                new CultureInfo("es-ES"),
                new CultureInfo("ca-ES"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("es-ES"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
