using Model.Entities;

namespace Model.Models.Cart;

public class CartModel
{
    public List<Dictionary<string, string>> ProductList { get; init; } = [];
    public DeliveryAddress? DeliveryAddress { get; set; }
}