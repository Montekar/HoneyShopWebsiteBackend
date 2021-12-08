using System.Collections.Generic;
using System.IO;
using HoneyShop.Core.IServices;
using HoneyShop.Core.Models;
using HoneyShop.Domain.IRepository;

namespace HoneyShop.Domain.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _repository;

        public ShoppingCartService(IShoppingCartRepository repository)
        {
            _repository = repository ?? throw new InvalidDataException("Shopping cart repository can not be null");
        }

        public List<ShoppingCart> GetAllItems()
        {
            return _repository.GetAllItems();
        }

        public ShoppingCart DeleteItem(int id)
        {
            return _repository.DeleteItem(id);
        }

        public ShoppingCart AddItem(ShoppingCart shoppingCartItem)
        {
            return _repository.AddItem(shoppingCartItem);
        }

        public ShoppingCart UpdateItem(ShoppingCart shoppingCartItem)
        {
            return _repository.UpdateItem(shoppingCartItem);
        }

        public ShoppingCart GetItemById(int id)
        {
            return _repository.GetItemById(id);
        }

        public bool EmptyCart()
        {
            return _repository.EmptyCart();
        }
    }
}