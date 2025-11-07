using ECommerceAPI.Core.Entities;
using System.Collections.Generic;

namespace ECommerceAPI.Core.Interfaces.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetActive();
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void SoftDelete(int id);
    }
}
