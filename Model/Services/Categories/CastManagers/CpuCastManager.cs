using Model.Cpu;
using Model.General;
using Model.Services.Categories.CastManagers.Interfaces;

namespace Model.Services.Categories.CastManagers;

public class CpuCastManager : BaseCastManager, ICpuCastManager
{
    public CpuFilterParams CastToCpuFilterParams(List<ParamBaseModel> param)
    {
        var cpuFilterParams = new CpuFilterParams();

        foreach (var p in param)
            switch (p.Name)
            {
                case "Sockets":
                    cpuFilterParams.Sockets.Add(p.Value);
                    break;
                case "Architectures":
                    cpuFilterParams.Architectures.Add(p.Value);
                    break;
                case "UnlockedMultiplier":
                    cpuFilterParams.UnlockedMultiplers.Add(p.Value);
                    break;
                case "Teams":
                    cpuFilterParams.Teams.Add(p.Value);
                    break;
            }

        return cpuFilterParams;
    }
}