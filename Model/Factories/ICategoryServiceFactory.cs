using Model.Services.Categories.Interfaces;

namespace Model.Factories;

public interface ICategoryServiceFactory
{
    ICategoryService GetService(string type);
}