using Model.DataTransfer;
using Model.Entities;
using Model.Models.General;

namespace Model.Services.Categories.CastManagers.Interfaces;

public interface ICpuCastManager
{
    CpuFiltersDto CastToCpuFilterParams(List<ParamBaseModel> param);
    string CastToJsonFormat(object obj);
    Cpu CastProductModelToCooler(ProductModel model);
}