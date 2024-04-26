using DataAccess.DTO;
using Model.Cart;
using Model.General;

namespace Model.Services.Interfaces;

public interface ICartService
{
    void Add(int productId, int quantity, string userId);
    void Remove(int productId, string userId);
    CartModel ReturnModel(string userId);
    void CreateOrder(UserOrderDataModel model);
}