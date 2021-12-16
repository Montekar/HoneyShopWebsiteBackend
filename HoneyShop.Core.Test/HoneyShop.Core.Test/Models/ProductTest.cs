using HoneyShop.Core.Models;
using Xunit;

namespace HoneyShop.Core.Test.Models
{
    public class ProductTest
    {
        private readonly Product _product;

        public ProductTest()
        {
            _product = new Product();
        }
         
        [Fact]
        public void ProductClass_Exists()
        {
            Assert.NotNull(_product);
        }
        
        [Fact]
        public void ProductClass_HasId_WithTypeInt()
        {
            var expected = 1;
            _product.Id = 1;
            Assert.Equal(expected, _product.Id);
            Assert.True(_product.Id is int);
        }

        [Fact]
        public void ProductClass_HasName_WithTypeString()
        {
            var expected = "Honey";
            _product.Name = "Honey";
            Assert.Equal(expected, _product.Name);
            Assert.True(_product.Name is string);
        }

        [Fact]
        public void ProductClass_HasDescription_WithTypeString()
        {
            var expected = "1kg Dark Honey";
            _product.Description = "1kg Dark Honey";
            Assert.Equal(expected, _product.Description);
            Assert.True(_product.Description is string);
        }

        [Fact]
        public void ProductClass_HasPrice_WithTypeDouble()
        {
            var expected = 5.00;
            _product.Price = 5.00;
            Assert.Equal(expected, _product.Price);
            Assert.True(_product.Price is double);
        }
    }
}