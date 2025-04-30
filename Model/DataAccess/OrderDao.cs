using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.DataAccess.Interfaces;
using Model.DataTransfer;

namespace Model.DataAccess;

public class OrderDao(DbContextOptions<PGPContext> options) : BaseDao(options), IOrderDao
{
    public List<OrdersBaseInfoDto> GetOrders(int userId)
    {
        var query = from o in PgpContext.Orders
            join od in PgpContext.OrderDetails on o.OrderId equals od.OrderId
            from product in (
                from cpu in PgpContext.Cpus
                where od.Type == "CPU" && cpu.ProductId == od.ProductId
                select new { cpu.Name, Image = cpu.ProductImg, cpu.Price }
            ).Concat(
                from gpu in PgpContext.Gpus
                where od.Type == "GPU" && gpu.ProductId == od.ProductId
                select new { gpu.Name, Image = gpu.ProductImg, gpu.Price }
            ).Concat(
                from cooler in PgpContext.Coolers
                where od.Type == "COOLER" && cooler.ProductId == od.ProductId
                select new { cooler.Name, Image = cooler.ProductImg, cooler.Price }
            )
            where o.UserId == userId
            select new OrdersBaseInfoDto
            {
                OrderId = o.OrderId,
                ProductId = od.ProductId,
                ProductName = product.Name,
                ProductImg = product.Image,
                Type = od.Type,
                CreationDate = o.CreationDate,
                Price = product.Price,
                Quantity = od.Quantity
            };

        var result = query.ToList();

        return result;
    }
}