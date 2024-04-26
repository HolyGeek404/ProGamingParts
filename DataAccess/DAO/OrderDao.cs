using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataAccess.DAO.Interfaces;
using DataAccess.DTO;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DAO;

public class OrderDao(IConfiguration configuration) : BaseDao(configuration), IOrderDao
{
    public void CreateOrder(int productId, string userId)
    {
        DbConnection.Execute("dbo.CreateOrder @productId, @userId", new { productId, userId });
    }

    public void PrepareOrder(PrepareOrderDto model)
    {
        DbConnection.Execute("dbo.PrepareOrder_InsertNewPrepareOrder @ProductId, @Quantity, @UserId",
            new { PrepareOrderDto.ProductId, PrepareOrderDto.Quantity, PrepareOrderDto.UserId });
    }

    public List<OrdersBaseInfoDto> GetOrders(string userId)
    {
        return DbConnection.Query<OrdersBaseInfoDto>("dbo.Order_SelectBaseInfo @UserId", new {UserId = userId}).ToList();
    }
}