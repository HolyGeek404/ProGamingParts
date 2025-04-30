using Model.DataTransfer;

namespace Model.DataAccess.Interfaces;

public interface IOrderDao
{ 
    List<OrdersBaseInfoDto> GetOrders(int userId);
}