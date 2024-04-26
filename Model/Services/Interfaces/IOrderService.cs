using DataAccess.DTO;
using Model.General;

namespace Model.Services.Interfaces;

public interface IOrderService
{
    void PrepareOrder(PrepareOrderModel model);
    void CreateOrder(int productId, string id);
    Dictionary<int,List<OrdersBaseInfoDto>> GetOrdersBaseInfoModel(string userId);
}