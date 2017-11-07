using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
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
            services.AddIdentity<ApplicationUser, ApplicationRole>()
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

            // forcing SSL for external logins
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
            
            // GOOGLE
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
               googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
               googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });

            // FACEBOOK
            services.AddAuthentication().AddFacebook( fbOptions => 
            {
                fbOptions.ClientId = Configuration["Authentication:Facebook:AppId"];
                fbOptions.ClientSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(10);
                 options.LoginPath = "/account";
                 options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = new PathString("/account");
                options.SlidingExpiration = true;
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            // forcing SSL for external logins
            var options = new RewriteOptions().AddRedirectToHttps();
            app.UseRewriter(options);

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
