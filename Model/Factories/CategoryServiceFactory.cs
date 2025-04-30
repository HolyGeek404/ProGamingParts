using Model.Services.Categories.Interfaces;

namespace Model.Factories;

public class CategoryServiceFactory(ICategoryServiceContainer _serviceContainer) : ICategoryServiceFactory
{
    public ICategoryService GetService(string type)
    {
        return _serviceContainer.RegisterService(type);
    }
}