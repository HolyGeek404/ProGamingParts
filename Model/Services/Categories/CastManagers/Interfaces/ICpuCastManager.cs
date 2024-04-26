﻿using Model.Cpu;
using Model.General;

namespace Model.Services.Categories.CastManagers.Interfaces;

public interface ICpuCastManager
{
    CpuFilterParams CastToCpuFilterParams(List<ParamBaseModel> param);
    string CastToJsonFormat(object obj);
}