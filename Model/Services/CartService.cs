using DataAccess.DAO.Interfaces;
using Model.Cart;
using Model.General;
using Model.Services.Interfaces;

namespace Model.Services;

public class CartService(ICartDao cartDao) : BaseService, ICartService
{
    public CartModel ReturnModel(string userId)
    {
        var x = cartDao.GetCart(userId);
       
        foreach (var cartDto in x)
        {
            cartDto.TotalPrice = cartDto.Price * cartDto.Quantity;
        }
        var productList = CreateListOfProducts(x.Cast<object>().ToList());

        return new CartModel
        {
             ProductList = productList
        };
    }
    
    public void Add(int productId, int quantity, string userId)
    {
        cartDao.AddToCart(productId, userId, quantity);
    }
    
    public void Remove(int productId, string userId)
    {
        cartDao.RemoveProduct(productId, userId);
    }

    public void CreateOrder(UserOrderDataModel model)
    {
        cartDao.CreateOrder(model);
    }
}