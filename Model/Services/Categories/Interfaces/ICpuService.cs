using Model.Models.General;

namespace Model.Services.Categories.Interfaces;

public interface ICpuService
{
    ProductModel ReturnModel();
    ProductModel GetProduct(int id);
    ProductModel GenerateListOfFilteredProducts(List<ParamBaseModel> param);
}