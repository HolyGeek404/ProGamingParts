using Model.General;
using Model.Gpu;
using Model.Services.Categories.CastManagers.Interfaces;

namespace Model.Services.Categories.CastManagers;

public class GpuCastManager : BaseCastManager, IGpuCastManager
{
    public GpuFilterParamsModel CastToGpuFilterParams(List<ParamBaseModel> param)
    {
        var gpuFilterParams = new GpuFilterParamsModel();

        foreach (var p in param)
            switch (p.Name)
            {
                case "Manufactures":
                    gpuFilterParams.Manufactures.Add(p.Value);
                    break;
                case "AmdGpuProcessorNames":
                    gpuFilterParams.GpuProcessorName.Add(p.Value);
                    break;

                case "NvidiaGpuProcessorNames":
                    gpuFilterParams.GpuProcessorName.Add(p.Value);
                    break;

                case "GpuProcessorLine":
                    gpuFilterParams.GpuProcessorName.Add(p.Value);
                    break;

                case "MemorySizes":
                    gpuFilterParams.MemorySizes.Add(p.Value);
                    break;

                case "MemoryTypes":
                    gpuFilterParams.MemoryTypes.Add(p.Value);
                    break;
                case "Teams":
                    gpuFilterParams.Teams.Add(p.Value);
                    break;
            }

        return gpuFilterParams;
    }
}