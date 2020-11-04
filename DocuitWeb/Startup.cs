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
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Authorization;
using DocuitWeb.Services;
using System.Security.Claims;
using System.Net.Http.Headers;

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
            //services.AddProtectedLocalStore(new EncryptionService(
            //    new KeyInfo("45BLO2yoJkvBwz99kBEMlNkxvL40vUSGaqr/WBu3+Vg=", "Ou3fn+I9SVicGWMLkFEgZQ==")));
            

            services.AddScoped<CompanyService>();
            services.AddScoped<UserService>();
            services.AddScoped<SecurityService>();
            services.AddScoped<StatusService>();
            services.AddScoped<ProjectService>();
            services.AddScoped<DossierService>();
            services.AddScoped<DossierElementService>();
            services.AddScoped<WorkingCenterService>();
            services.AddScoped<BuildingTypeService>();
            services.AddScoped<CustomAuthenticationStateProvider>();
            services.AddScoped<AccessService>();
            services.AddScoped<StorageTools>();

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

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            //services.AddSingleton<HttpClient>();
            services.AddProtectedBrowserStorage();
            services.AddAuthentication(options=>
                {
                    
                }
            );

            services.AddAuthorization(options =>
                {
                    options.AddPolicy("administrator", policy => policy.RequireClaim(ClaimTypes.Role, "0"));
                    options.AddPolicy("manager", policy => policy.RequireClaim(ClaimTypes.Role, "1"));
                    options.AddPolicy("operator", policy => policy.RequireClaim(ClaimTypes.Role, "2"));
                    options.AddPolicy("guest", policy => policy.RequireClaim(ClaimTypes.Role, "3"));
                }
            );

            services.AddHttpClient("DocuItService", options =>
                {
                    options.BaseAddress = new Uri(Configuration["AppSettings:DocuItServiceServer"]);
                    options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
             );

            services.AddScoped<MyHttp>();

            services.AddScoped<TestingService>();
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
