using System.Collections.Generic;
using DataAccess.DTO;

namespace DataAccess.DAO.Interfaces;

public interface ICartDao
{
    void AddToCart(int productId, string userId, int quantity);
    List<CartDto> GetCart(string? userId);
    void RemoveProduct(int productId, string? userId);
    void CreateOrder(UserOrderDataDto model);
}