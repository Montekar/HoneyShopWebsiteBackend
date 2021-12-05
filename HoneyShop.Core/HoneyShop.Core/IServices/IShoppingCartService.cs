using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.Core.IServices
{
    public interface IShoppingCartService
    {
        List<ShoppingCart> GetAllItems();
        ShoppingCart DeleteItem(int id);
        ShoppingCart AddItem(ShoppingCart shoppingCartItem);
        ShoppingCart UpdateItem(ShoppingCart shoppingCartItem);
        ShoppingCart GetItemById(int id);
        bool EmptyCart();
    }
}