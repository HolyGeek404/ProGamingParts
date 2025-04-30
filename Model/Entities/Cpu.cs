using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public partial class Cpu
{
    [Key]
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string Team { get; set; }
    public string Type { get; private set; } = "CPU";

    public int Price { get; set; }

    public string Familiy { get; set; } = null!;

    public string Series { get; set; } = null!;

    public string Socket { get; set; } = null!;

    public string SupportedChipsets { get; set; } = null!;

    public string RecomendedChipset { get; set; } = null!;

    public string Architecture { get; set; } = null!;

    public string Frequency { get; set; } = null!;

    public string Cores { get; set; } = null!;

    public string Threads { get; set; } = null!;

    public string UnlockedMultipler { get; set; } = null!;

    public string CacheMemory { get; set; } = null!;

    public string IntegredGpu { get; set; } = null!;

    public string IntegredGpuModel { get; set; } = null!;

    public string SupportedRam { get; set; } = null!;

    public string Litography { get; set; } = null!;

    public string Tdp { get; set; } = null!;

    public string AdditionalInfo { get; set; } = null!;

    public string IncludedCooler { get; set; } = null!;

    public string Warranty { get; set; } = null!;

    public string ProductImg { get; set; } = null!;
}
