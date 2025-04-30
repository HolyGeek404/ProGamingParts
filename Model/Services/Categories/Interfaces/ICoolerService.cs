using Model.Models.General;

namespace Model.Services.Categories.Interfaces;

public interface ICoolerService
{
    ProductModel ReturnModel();
    ProductModel GetProduct(int id);
    ProductModel GenerateListOfFilteredProducts(List<ParamBaseModel> param);
    List<Dictionary<string, string>> CreateListOfProducts(List<object> productList);
    ProductModel CreateProduct(object product);
}