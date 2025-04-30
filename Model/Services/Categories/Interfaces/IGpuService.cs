using Model.Models.General;

namespace Model.Services.Categories.Interfaces;

public interface IGpuService
{
    ProductModel ReturnModel();
    ProductModel GetProduct(int id);
    ProductModel GenerateListOfFilteredProducts(List<ParamBaseModel> param);
}