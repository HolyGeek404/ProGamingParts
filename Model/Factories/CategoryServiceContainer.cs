using Microsoft.Extensions.DependencyInjection;
using Model.DataAccess.Interfaces;
using Model.Services.Categories;
using Model.Services.Categories.CastManagers.Interfaces;
using Model.Services.Categories.FilterManagers.Interfaces;
using Model.Services.Categories.Interfaces;

namespace Model.Factories;

public class CategoryServiceContainer(IServiceProvider _serviceProvider) : ICategoryServiceContainer
{
    public ICategoryService RegisterService(string type)
    {
        return type.ToLower() switch
        {
            "gpu" => new GpuService(
                _serviceProvider.GetService<IGpuFilterManager>(),
                _serviceProvider.GetService<IGpuDao>(),
                _serviceProvider.GetService<IGpuCastManager>()),

            "cpu" => new CpuService(
                _serviceProvider.GetService<ICpuFilterManager>(),
                _serviceProvider.GetService<ICpuDao>(),
                _serviceProvider.GetService<ICpuCastManager>()),

            "cooler" => new CoolerService(
                _serviceProvider.GetService<ICoolerFilterManager>(),
                _serviceProvider.GetService<ICoolerDao>(),
                _serviceProvider.GetService<ICoolerCastManager>()),
            _ => null
        };
    }
}