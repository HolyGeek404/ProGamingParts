using Model.DataAccess.Interfaces;
using Model.DataTransfer;
using Model.Models.Cart;
using Model.Services.General;
using Model.Services.Interfaces;

namespace Model.Services.User;

public class CartService(ICartDao cartDao, IUserDao userDao) : BaseCategoryService, ICartService
{
    public CartModel ReturnModel(string userId)
    {
        var x = cartDao.GetCart(userId);

        foreach (var cartDto in x)
        {
            cartDto.TotalPrice = cartDto.Price * cartDto.Quantity;
        }
        var productList = CreateListOfProducts(x.Cast<object>().ToList());
        var defaultDeliveryAddress = userDao.GetDefaultDeliveryAddress(userId);

        return new CartModel
        {
            ProductList = productList,
            DeliveryAddress = defaultDeliveryAddress
        };
    }

    public void Add(AddCartModel addCartModel, string userId)
    {
        cartDao.AddToCart(addCartModel, userId);
    }

    public void Remove(int productId, string userId)
    {
        cartDao.RemoveProduct(productId, userId);
    }

    public void CreateOrder(UserOrderDataDto model)
    {
        cartDao.CreateOrder(model);
    }
}