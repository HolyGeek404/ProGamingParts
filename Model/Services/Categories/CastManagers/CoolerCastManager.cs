using Model.DataTransfer;
using Model.Entities;
using Model.Models.General;
using Model.Services.Categories.CastManagers.Interfaces;

namespace Model.Services.Categories.CastManagers;

public class CoolerCastManager : BaseCastManager, ICoolerCastManager
{
    public CoolerFilterDto CastToCoolerFilterParams(List<ParamBaseModel> param)
    {
        var coolerFilterParams = new CoolerFilterDto();

        foreach (var p in param)
            switch (p.Name)
            {
                case "Manufactures":
                    coolerFilterParams.Manufacture.Add(p.Value);
                    break;
                case "CoolerTypes":
                    coolerFilterParams.CoolerTypes.Add(p.Value);
                    break;
                case "PriceMin":
                    coolerFilterParams.PriceMin = int.Parse(p.Value);
                    break;
                case "PriceMax":
                    coolerFilterParams.PriceMax = int.Parse(p.Value);
                    break;
            }
        return coolerFilterParams;
    }

    public Cooler CastProductModelToCooler(ProductModel model)
    {
        var cooler = new Cooler
        {
            ProductId = int.Parse(model.AllParameters["ProductId"]),
            Name = model.AllParameters["Name"],
            Team = model.AllParameters["Team"],
            CoolerType = model.AllParameters["CoolerType"],
            Compatibility = model.AllParameters["Compatibility"],
            Size = model.AllParameters["Size"],
            HeatPipes = model.AllParameters["HeatPipes"],
            Fans = model.AllParameters["Fans"],
            Rpmcontroll = model.AllParameters["Rpmcontroll"],
            Rmp = model.AllParameters["Rmp"],
            BearingType = model.AllParameters["BearingType"],
            FanSize = model.AllParameters["FanSize"],
            Connector = model.AllParameters["Connector"],
            SupplyVoltage = model.AllParameters["SupplyVoltage"],
            SupplyCurrent = model.AllParameters["SupplyCurrent"],
            Highlight = model.AllParameters["Highlight"],
            Mtbflifetime = model.AllParameters["Mtbflifetime"],
            Height = model.AllParameters["Height"],
            Width = model.AllParameters["Width"],
            Depth = model.AllParameters["Depth"],
            Weight = model.AllParameters["Weight"],
            Warranty = model.AllParameters["Warranty"],
            ProducentCode = model.AllParameters["ProducentCode"],
            ProductImg = model.AllParameters["ProductImg"],
            Price = int.Parse(model.AllParameters["Price"]),
            Manufacture = model.AllParameters["Manufacture"]
        };

        return cooler;
    }
}