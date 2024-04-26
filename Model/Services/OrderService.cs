using DataAccess.DAO.Interfaces;
using DataAccess.DTO;
using Model.General;
using Model.Services.Interfaces;

namespace Model.Services;

public class OrderService(IOrderDao orderDao) : IOrderService
{
    private IOrderDao OrderDao { get; } = orderDao;

    public void PrepareOrder(PrepareOrderModel model)
    {
        OrderDao.PrepareOrder(model);
    }

    public void CreateOrder(int productId, string id)
    {
        OrderDao.CreateOrder(productId, id);
    }

    public  Dictionary<int,List<OrdersBaseInfoDto>> GetOrdersBaseInfoModel(string userId)
    {
        var orderBaseInfoList = OrderDao.GetOrders(userId);
        var groupedByOrderId = orderBaseInfoList.GroupBy(o => o.OrderId).ToDictionary(g => g.Key, g => g.ToList());

        return groupedByOrderId;
    }
}