using Model.DataTransfer;
using Model.Models.Cart;

namespace Model.DataAccess.Interfaces;

public interface ICartDao
{
    public void AddToCart(AddCartModel addCartModel, string type);
    List<CartDto> GetCart(string? userId);
    void RemoveProduct(int productId, string? userId);
    void CreateOrder(UserOrderDataDto model);
}