using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PBook.Domain.Excel;
using PBook.Domain.Services;
using PBook.Infra;
using PBook.UI.Repositorio;

namespace PBook.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<BancoContent>(
                    options => options.UseSqlServer(Configuration.GetConnectionString("DataBase")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<IAssuntoRepository, AssuntoRepository>();
            services.AddScoped<ILivroAssuntoRepository, LivroAssuntoRepository>();
            services.AddScoped<ILivroAutorRepository, LivroAutorRepository>();

            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<IAssuntoService, AssuntoService>();

            services.AddScoped<IExcelService, ExcelService>();

            services.AddSession(o =>
            {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
