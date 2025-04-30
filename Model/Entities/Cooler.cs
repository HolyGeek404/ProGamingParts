using System.ComponentModel.DataAnnotations;

namespace Model.Entities;

public class Cooler
{
    [Key]
    public int ProductId { get; set; }
    public string Name { get; set; } = "";
    public string Team { get; set; } = "";
    public string Type { get; private set; } = "COOLER";
    public string CoolerType { get; set; } = "";
    public string Compatibility { get; set; } = "";
    public string Size { get; set; } = "";
    public string HeatPipes { get; set; } = "";
    public string Fans { get; set; } = "";
    public string Rpmcontroll { get; set; } = "";
    public string Rmp { get; set; } = "";
    public string BearingType { get; set; } = "";
    public string FanSize { get; set; } = "";
    public string Connector { get; set; } = "";
    public string SupplyVoltage { get; set; } = "";
    public string SupplyCurrent { get; set; } = "";
    public string Highlight { get; set; } = "";
    public string Mtbflifetime { get; set; } = "";
    public string Height { get; set; } = "";
    public string Width { get; set; } = "";
    public string Depth { get; set; } = "";
    public string Weight { get; set; } = "";
    public string Warranty { get; set; } = "";
    public string ProducentCode { get; set; } = "";
    public string ProductImg { get; set; } = "";
    public int Price { get; set; }
    public string Manufacture { get; set; } = "";
}
