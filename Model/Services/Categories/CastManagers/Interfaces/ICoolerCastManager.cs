using Model.DataTransfer;
using Model.Entities;
using Model.Models.General;

namespace Model.Services.Categories.CastManagers.Interfaces;

public interface ICoolerCastManager
{
    CoolerFilterDto CastToCoolerFilterParams(List<ParamBaseModel> param);
    string CastToJsonFormat(object obj);
    public Cooler CastProductModelToCooler(ProductModel model);
}