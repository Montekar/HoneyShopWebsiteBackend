using System.Collections.Generic;
using HoneyShop.Core.Models;

namespace HoneyShop.Domain.IRepository
{
    public interface IShoppingCartRepository
    {
        List<ShoppingCart> GetAllItems();
        ShoppingCart DeleteItem(int id);
        ShoppingCart AddItem(ShoppingCart shoppingCartItem);
        ShoppingCart UpdateItem(ShoppingCart shoppingCartItem);
        ShoppingCart GetItemById(int id);
        bool EmptyCart();
    }
}