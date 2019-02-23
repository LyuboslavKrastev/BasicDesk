using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasicDesk.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using BasicDesk.App.Areas.Identity.Services;
using BasicDesk.Data.Models;
using BasicDesk.App.Common;
using BasicDesk.App.Hubs;
using BasicDesk.Services;
using BasicDesk.Services.Repository;
using BasicDesk.Services.AutoMapping;
using BasicDesk.Services.Interfaces;
using BasicDesk.App.Common.Interfaces;
using BasicDesk.App.Common.Attributes;

namespace BasicDesk.App
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

            services.AddDbContext<BasicDeskDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("BasicDeskConnection")));

            services
            .AddIdentity<User, IdentityRole>()
            .AddDefaultUI()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<BasicDeskDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions
                {
                    RequiredLength = 6,
                    RequiredUniqueChars = 1,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false
                };

                options.SignIn.RequireConfirmedEmail = false;
                options.Lockout.AllowedForNewUsers = true;
            });

            /* 
                Transient objects are always different; a new instance is provided to every controller and every service.
                Scoped objects are the same within a request, but different across different requests.
                Singleton objects are the same for every object and every request.
            */
            services.AddSingleton<IEmailSender, VerificationEmailSender>();
            services.Configure<EmailSenderOptions>(this.Configuration.GetSection("EmailSettings"));
            services.AddScoped(typeof(DbRepository<>), typeof(DbRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<ISolutionService, SolutionService>();
            services.AddScoped<IApprovalService, ApprovalService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IAlerter, Alerter>();
            services.AddScoped<RequestSorter, RequestSorter>();
            services.AddScoped<ILogger, Logger>();
            services.AddScoped(typeof(AttachmentService<>));
            services.AddScoped<IFileUploader, FileUploader>();
            services.AddScoped<StatusService>();


            AutoMapperConfig.RegisterMappings();
            services.AddSignalR();
            services.AddMvc(options => 
            {
                options.Filters.Add(new CustomExceptionFilterAttribute(new Logger()));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
            });

            if (env.IsDevelopment())
            {
                app.SeedDatabase();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
