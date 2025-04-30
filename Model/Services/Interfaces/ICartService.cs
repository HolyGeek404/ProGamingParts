using Model.DataTransfer;
using Model.Models.Cart;

namespace Model.Services.Interfaces;

public interface ICartService
{
    void Add(AddCartModel addCartModel, string userId);
    void Remove(int productId, string userId);
    CartModel ReturnModel(string userId);
    void CreateOrder(UserOrderDataDto model);
}