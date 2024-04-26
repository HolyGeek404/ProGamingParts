using Model.General;
using Model.Gpu;

namespace Model.Services.Categories.CastManagers.Interfaces;

public interface IGpuCastManager
{
    GpuFilterParamsModel CastToGpuFilterParams(List<ParamBaseModel> param);
    string CastToJsonFormat(object obj);
}