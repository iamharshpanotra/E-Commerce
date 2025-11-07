using ECommerceAPI.Core.Entities;
using System.Collections.Generic;

namespace ECommerceAPI.Core.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(string action, int? id = null);
        void ManageProduct(string action, Product product);
    }
}
