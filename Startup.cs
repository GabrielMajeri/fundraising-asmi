using System.Net;
using System.Threading.Tasks;
using Asmi.Fundraising.Data;
using Asmi.Fundraising.Models;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartBreadcrumbs.Extensions;

namespace Asmi.Fundraising
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("FundraisingDB");
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    var googleAuth = Configuration.GetSection("Authentication:Google");
                    options.ClientId = googleAuth["ClientId"];
                    options.ClientSecret = googleAuth["ClientSecret"];

                    options.Events = new OAuthEvents
                    {
                        // Make the authentication client only display @asmi.ro addresses.
                        OnRedirectToAuthorizationEndpoint = context =>
                        {
                            context.Response.Redirect(context.RedirectUri + "&hd=" + WebUtility.UrlEncode("asmi.ro"));

                            return Task.CompletedTask;
                        },
                    };
                });

            services.AddControllers();
            services.AddRazorPages(options =>
            {
                options.Conventions.AllowAnonymousToPage("/Index");
                options.Conventions.AllowAnonymousToPage("/AccessDenied");
            });

            services.AddBreadcrumbs(GetType().Assembly);

            services.AddTransient<SeedUserRoles>();
            services.AddTransient<SeedData>();

            services.AddScoped<ImageUploadService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            AppDbContext context, SeedUserRoles seedUserRoles, SeedData seedData)
        {
            if (context.Database.EnsureCreated())
            {
                seedUserRoles.Seed();
                seedData.Seed();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStatusCodePages();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapRazorPages()
                    .RequireAuthorization();
            });
        }
    }
}
