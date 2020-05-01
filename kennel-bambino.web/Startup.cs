using kennel_bambino.web.Data;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace kennel_bambino.web
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllers();

            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/Admin");
            });

            services.AddTransient<IBodyTypeService, BodyTypeService>();
            services.AddTransient<IEyeColorService, EyeColorService>();
            services.AddTransient<IPatternService, PatternService>();
            services.AddTransient<ICarouselService, CarouselService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IPetService, PetService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
