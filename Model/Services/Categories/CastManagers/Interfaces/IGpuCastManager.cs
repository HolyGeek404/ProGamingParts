using Model.DataTransfer;
using Model.Entities;
using Model.Models.General;

namespace Model.Services.Categories.CastManagers.Interfaces;

public interface IGpuCastManager
{
    GpuFiltersDto CastToGpuFilterParams(List<ParamBaseModel> param);
    string CastToJsonFormat(object obj);
    Gpu CastProductModelToCooler(ProductModel model);
}