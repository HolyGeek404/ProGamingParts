using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Model.Contexts;
using Model.DataAccess;
using Model.DataAccess.Interfaces;
using Model.Factories;
using Model.Services.ApiServices;
using Model.Services.ApiServices.Interfaces;
using Model.Services.Categories;
using Model.Services.Categories.CastManagers;
using Model.Services.Categories.CastManagers.Interfaces;
using Model.Services.Categories.FilterManagers.Cooler;
using Model.Services.Categories.FilterManagers.CPU;
using Model.Services.Categories.FilterManagers.GPU;
using Model.Services.Categories.FilterManagers.Interfaces;
using Model.Services.General;
using Model.Services.Interfaces;
using Model.Services.User;

namespace WebLibrary;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        #region DI

        services.AddDbContext<PGPContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("AzureCloudConnection")));

        services.AddTransient<CpuService>();
        services.AddTransient<GpuService>();
        services.AddTransient<CoolerService>();
        services.AddTransient<IProductManagmentService, ProductManagmentService>();
        services.AddTransient<ICategoryServiceContainer, CategoryServiceContainer>();
        services.AddTransient<ICategoryServiceFactory, CategoryServiceFactory>();

        services.AddScoped<IGpuDao, GpuDao>();
        services.AddScoped<ICpuDao, CpuDao>();
        services.AddScoped<IProductManagmentDao, ProductManagmentDao>();
        services.AddScoped<IHomeDao, HomeDao>();
        services.AddScoped<IUserDao, UserDao>();
        services.AddScoped<ICartDao, CartDao>();
        services.AddScoped<IOrderDao, OrderDao>();
        services.AddScoped<ICoolerDao, CoolerDao>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ICpuFilterManager, CpuFilterManager>();
        services.AddScoped<ICpuCastManager, CpuCastManager>();
        services.AddScoped<IGpuFilterManager, GpuFilterManager>();
        services.AddScoped<IGpuCastManager, GpuCastManager>();
        services.AddScoped<ICoolerFilterManager, CoolerFilterManager>();
        services.AddScoped<ICoolerCastManager, CoolerCastManager>();
        services.AddScoped<IHomeApiService, HomeApiService>();
        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IAccountVerifycationService, AccountVerificationService>();
        #endregion

        services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"));

      
        services.AddHttpContextAccessor();
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddControllersWithViews().AddRazorRuntimeCompilation();
        services.AddSession();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseExceptionHandler("/Error/Index"); 
        app.UseStatusCodePagesWithRedirects("/Error/Index");

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