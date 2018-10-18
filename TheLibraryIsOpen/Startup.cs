using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheLibraryIsOpen.Controllers.StorageManagement;
using TheLibraryIsOpen.Database;
using TheLibraryIsOpen.Models.DBModels;
using Microsoft.EntityFrameworkCore;
using TheLibraryIsOpen.Models;

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

            //services.AddScoped(typeof(Microsoft.AspNet.Identity.UserManager<Client>), typeof(ClientManager));
            //services.AddScoped(typeof(Microsoft.AspNet.Identity.IUserStore<Client>), typeof(ClientStore));
            services.AddSingleton(typeof(ClientManager));
            services.AddSingleton(typeof(ClientStore));
            services.AddSingleton(typeof(ClientSignInManager));
            services.AddSingleton(typeof(MovieCatalog));

            services.AddSingleton(typeof(MagazineCatalog));
            services.AddSingleton(typeof(BookCatalog));
            services.AddSingleton(typeof(MusicCatalog));

            services.AddSingleton(typeof(Db));

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

		    
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

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

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
	}
}
