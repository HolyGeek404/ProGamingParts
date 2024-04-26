using DataAccess.DAO;
using DataAccess.DAO.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model.Services;
using Model.Services.Categories;
using Model.Services.Categories.CastManagers;
using Model.Services.Categories.CastManagers.Interfaces;
using Model.Services.Categories.FilterManagers;
using Model.Services.Categories.FilterManagers.Interfaces;
using Model.Services.Categories.Interfaces;
using Model.Services.Interfaces;

namespace WebLibrary;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IGpuDao, GpuDao>();
        services.AddSingleton<ICpuDao, CpuDao>();
        services.AddSingleton<IHomeDao, HomeDao>();
        services.AddSingleton<IUserDao, UserDao>();
        services.AddSingleton<ICartDao, CartDao>();
        services.AddSingleton<IOrderDao, OrderDao>();
        services.AddSingleton<ICpuService, CpuService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IGpuService, GpuService>();
        services.AddSingleton<IHomePageService, HomePageService>();
        services.AddSingleton<IHashService, HashService>();        
        services.AddSingleton<IOrderService, OrderService>();        
        services.AddSingleton<ICartService, CartService>();        
        services.AddSingleton<ICpuFilterManager, CpuFilterManager>();
        services.AddSingleton<ICpuCastManager, CpuCastManager>();
        services.AddSingleton<IGpuFilterManager, GpuFilterManager>();
        services.AddSingleton<IGpuCastManager, GpuCastManager>();

        services.AddSingleton<IBaseDao>(_ => new BaseDao(Configuration));

        services.AddHttpContextAccessor();
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddControllersWithViews().AddRazorRuntimeCompilation();
        services.AddSession();

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

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseSession();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
    }
}