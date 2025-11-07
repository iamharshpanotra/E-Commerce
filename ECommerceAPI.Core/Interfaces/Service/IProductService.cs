using ECommerceAPI.Core.DTOs;
using ECommerceAPI.Core.Entities;
using System.Collections.Generic;

namespace ECommerceAPI.Core.Interfaces.Service
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAll();
        IEnumerable<ProductDto> GetActive();
        ProductDto GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void SoftDelete(int id);
    }
}
