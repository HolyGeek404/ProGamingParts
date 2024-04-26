using System.Collections.Generic;
using System.Linq;
using Dapper;
using DataAccess.DAO.Interfaces;
using DataAccess.DTO;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DAO;

public class CartDao(IConfiguration configuration) : BaseDao(configuration), ICartDao
{
    public void AddToCart(int productId, string userId, int quantity)
    {
        DbConnection.Query("dbo.Cart_AddToCart @productId, @userId, @quantity", new
        {
            productId = productId, userId = userId, quantity = quantity
        });
    }

    public List<CartDto> GetCart(string? userId)
    {
        return DbConnection.Query<CartDto>("dbo.Cart_GetCart @userId", 
            new { userId = userId }).ToList();
    }

    public void RemoveProduct(int productId, string? userId)
    {
        DbConnection.Query("dbo.Cart_RemoveFromCart @productId, @userId", new
        {
            productId = productId, userId = userId
        });
    }

    public void CreateOrder(UserOrderDataDto model)
    {
        var currentOrderId = DbConnection.Query<int>("dbo.Order_InsertOrder @Name, @Surname, @Street, " +
                           "@Block, @Flat, @City, @PostalCode, @Pay, @Shipping, @UserId", new
        {
            Name = model.Name,
            Surname = model.Surname,
            Street = model.Street,
            Block = model.Block,
            Flat = model.Flat,
            City = model.City,
            PostalCode = model.PostalCode,
            Pay = model.Pay,
            Shipping = model.Shipping,
            UserId = model.UserId
        }).First();
        
        foreach (var detail in model.Details)
        {
            DbConnection.Query("dbo.Order_InsertOrderDetails @ProductId, @Quantity, @UserId, @OrderId", new
            {
                OrderId = currentOrderId,
                UserId = model.UserId,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity
            });
        }
    }
}