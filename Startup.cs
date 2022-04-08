using Intex2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ML.OnnxRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2
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
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                // requires using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllersWithViews();
            services.AddDbContext<UtahCrashDbContext>(options =>
           {
               options.UseMySql(Configuration["ConnectionStrings:UtahCrashDbConnection"]);
           });

            services.AddDbContext<AppIdentityDBContext>(options =>
            { 
                options.UseMySql(Configuration["ConnectionStrings:IdentityConnection"]);
            });
        
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDBContext>();

            services.AddScoped<ICrashRepository, EFCrashRepository>();

            services.AddSingleton<InferenceSession>(
                new InferenceSession("utahcrash.onnx"));
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {

                context.Response.Headers.Add(
                    "Content-Security-Policy",
                    "default-src *; " +
                    "script-src * 'unsafe-inline'; " +
                    "style-src * 'unsafe-inline'; " +
                    "img-src *" +
                    "object-src *;" +
                    "script-src *;" +
                    "style-src *; +" +
                    "upgrade-insecure-requests");

                await next();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            //app.Use(async (ctx, next) =>
            //{ ctx.Response.Headers.Add("Content-Security-Policy",
            //    "default-src *")
            //    ; await next(); });
            //app.Use(async (ctx, next) =>
            //{ ctx.Response.Headers.Add
            //    ("Content-Security-Policy",
            //    "default-src 'self'" +
            //    "img-src *.history.com");

            //await next(); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{crashId?}");
            });

            IdentitySeedData.EnsurePopulated(app);



        }
    }
}
