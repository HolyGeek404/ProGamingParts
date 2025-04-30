using Model.DataAccess.Interfaces;
using Model.DataTransfer;
using Model.Services.Interfaces;

namespace Model.Services.User;

public class OrderService(IOrderDao orderDao) : IOrderService
{
    private IOrderDao OrderDao { get; } = orderDao;

    public Dictionary<int, List<OrdersBaseInfoDto>> GetOrdersBaseInfoModel(int userId)
    {
        var orderBaseInfoList = OrderDao.GetOrders(userId);
        var groupedByOrderId = orderBaseInfoList.GroupBy(o => o.OrderId).ToDictionary(g => g.Key, g => g.ToList());

        return groupedByOrderId;
    }

    public List<OrdersBaseInfoDto> GetOrdersBaseInfoModelJson(int userId)
    {
        return OrderDao.GetOrders(userId);
    }
}