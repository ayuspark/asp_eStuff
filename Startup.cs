using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using asp_ecommerce.Models;


namespace asp_ecommerce
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSession();

            //DB context
            services.AddDbContext<ApplicationDbContext>(options =>
                                                        options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            //Add Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                //password setting
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 4;
                //options.Password.RequireNonAlphanumeric = true;

                //lockout setting
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = false;

                //user setting
                options.User.RequireUniqueEmail = true;
            });

            // services.ConfigureApplicationCookie(options =>
            // {
            //     options.Cookie.HttpOnly = true;
            //     options.Cookie.Expiration = TimeSpan.FromDays(10);
            //     // options.LoginPath = "/user/login";
            //     // options.LogoutPath = "/user/logout";
            //     options.AccessDeniedPath = "/";
            //     options.SlidingExpiration = true;
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc((routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
            }));
        }
    }
}
