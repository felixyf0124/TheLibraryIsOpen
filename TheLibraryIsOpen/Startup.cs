using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TheLibraryIsOpen.Controllers.StorageManagement;
using TheLibraryIsOpen.db;
using TheLibraryIsOpen.Models.DBModels;
using TheLibraryIsOpen.Models.Search;

namespace TheLibraryIsOpen
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
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDefaultIdentity<Client>()
                .AddUserStore<ClientStore>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddSingleton(typeof(Db));
            services.AddSingleton(typeof(Search));
            services.AddScoped(typeof(UnitOfWork));
            services.AddScoped(typeof(IdentityMap));

            services.AddScoped(typeof(ClientManager));
            services.AddScoped(typeof(ClientStore));
            services.AddScoped(typeof(MovieCatalog));

            services.AddScoped(typeof(MagazineCatalog));
            services.AddScoped(typeof(BookCatalog));
            services.AddScoped(typeof(MusicCatalog));
            services.AddScoped(typeof(PersonCatalog));
            services.AddScoped(typeof(ModelCopyCatalog));
            services.AddScoped(typeof(TransactionCatalog));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ClientManager cm)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            try
            {

                string email = Configuration["DefaultAdmin:Email"];
                string password = Configuration["DefaultAdmin:Password"];

                Client x = await cm.FindByEmailAsync(email);

                if (x == null)
                {
                    await cm.CreateAsync(new Client
                    {
                        EmailAddress = email,
                        Password = password,
                        IsAdmin = true
                    });
                }
                else if (!x.IsAdmin)
                {

                    x.IsAdmin = true;
                    await cm.UpdateAsync(x);

                }
            }
            catch (ArgumentNullException)
            {
                var bg = Console.BackgroundColor;
                var fg = Console.ForegroundColor;

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("appsettings.json was not found. Skipping adding default admin.");

                Console.BackgroundColor = bg;
                Console.ForegroundColor = fg;
            }
        }
    }
}
