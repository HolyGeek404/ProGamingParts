using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.DataAccess.Interfaces;
using Model.DataTransfer;
using Model.Entities;
using Model.Models.Cart;

namespace Model.DataAccess;

public class CartDao(DbContextOptions<PGPContext> options) : BaseDao(options), ICartDao
{
    public void AddToCart(AddCartModel addCartModel, string userId)
    {
        var cart = new Cart
        {
            UserId = int.Parse(userId),
            Type = addCartModel.type,
            ProductId = int.Parse(addCartModel.productId),
            Quantity = int.Parse(addCartModel.quantity)
        };

        PgpContext.Carts.Add(cart);
        PgpContext.SaveChanges();
    }

    public List<CartDto> GetCart(string? userId)
    {
        var cartDtoList =
            (from c in PgpContext.Carts
             where c.UserId == int.Parse(userId)
             join g in PgpContext.Gpus on c.ProductId equals g.ProductId into gpuJoin
             from g in gpuJoin.DefaultIfEmpty()
             join cp in PgpContext.Cpus on c.ProductId equals cp.ProductId into cpuJoin
             from cp in cpuJoin.DefaultIfEmpty()
             join cr in PgpContext.Coolers on c.ProductId equals cr.ProductId into coolerJoin
             from cr in coolerJoin.DefaultIfEmpty()
             select new CartDto
             {
                 ProductId = c.ProductId,
                 Quantity = c.Quantity,
                 Type = c.Type,
                 Name = c.Type == "GPU" ? g.Name : c.Type == "CPU" ? cp.Name : cr.Name,
                 Price = c.Type == "GPU" ? g.Price : c.Type == "CPU" ? cp.Price : cr.Price,
                 ProductImg = c.Type == "GPU" ? g.ProductImg : c.Type == "CPU" ? cp.ProductImg : cr.ProductImg,
                 TotalPrice = c.Quantity * (c.Type == "GPU" ? g.Price : c.Type == "CPU" ? cp.Price : cr.Price)
             }).ToList();

        return cartDtoList;
    }

    public void RemoveProduct(int productId, string? userId)
    {
        var productFromCart = PgpContext.Carts.SingleOrDefault(x => x.ProductId == productId && x.UserId == int.Parse(userId));
        PgpContext.Carts.Remove(productFromCart);
        PgpContext.SaveChanges();
    }

    public void CreateOrder(UserOrderDataDto model)
    {
        var newOrder = new Order
        {
            UserId = model.UserId,
            Name = model.Name,
            CreationDate = DateTime.Today,
            Surname = model.Surname,
            Street = model.Street,
            Block = model.Block,
            Flat = model.Flat,
            City = model.City,
            PostalCode = model.PostalCode,
            Pay = model.Pay,
            Shipping = model.Shipping

        };

        PgpContext.Orders.Add(newOrder);
        PgpContext.SaveChanges();

        var currentOrderId = newOrder.OrderId;

        foreach (var newOrderDetails in model.Details.Select(detail => new OrderDetail
        {
            ProductId = detail.ProductId,
            Type = detail.Type,
            OrderId = currentOrderId,
            Quantity = detail.Quantity
        }))
        {
            PgpContext.OrderDetails.Add(newOrderDetails);
            PgpContext.SaveChanges();
        }

        var cartToRemove = PgpContext.Carts.Where(x => x.UserId == model.UserId);
        PgpContext.Carts.RemoveRange(cartToRemove);
        PgpContext.SaveChanges();
    }
}