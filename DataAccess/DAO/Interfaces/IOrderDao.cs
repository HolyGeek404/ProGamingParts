using System.Collections.Generic;
using DataAccess.DTO;

namespace DataAccess.DAO.Interfaces;

public interface IOrderDao
{
    void CreateOrder(int productId, string userId);
    void PrepareOrder(PrepareOrderDto model);
    List<OrdersBaseInfoDto> GetOrders(string userId);
}