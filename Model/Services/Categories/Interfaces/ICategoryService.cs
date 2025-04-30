using Model.Models.General;

namespace Model.Services.Categories.Interfaces;

public interface ICategoryService
{
    ProductModel ReturnModel();
    ProductModel GetProduct(int id);
    ProductModel GenerateListOfFilteredProducts(List<ParamBaseModel> param);

    void SaveProduct(ProductModel model);
    void AddNewProduct(ProductModel convertedModel);
    void Delete(int id);
}