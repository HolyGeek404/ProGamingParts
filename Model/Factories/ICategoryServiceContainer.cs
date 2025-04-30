using Model.Services.Categories.Interfaces;

namespace Model.Factories;

public interface ICategoryServiceContainer
{
    ICategoryService RegisterService(string type);
}