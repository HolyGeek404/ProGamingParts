using Model.DataTransfer;

namespace Model.Services.Interfaces;

public interface IOrderService
{
    Dictionary<int, List<OrdersBaseInfoDto>> GetOrdersBaseInfoModel(int userId);
    List<OrdersBaseInfoDto> GetOrdersBaseInfoModelJson(int userId);
}