using System.Collections.Generic;
using ECommerceAPI.Core.Entities;
using ECommerceAPI.Core.Interfaces;

namespace ECommerceAPI.Application.Services
{
    public class ProductService
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductService(IGenericRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepo.GetAll();
        }

        public Product GetProductById(int id)
        {
            return _productRepo.GetById(id);
        }

        public void AddProduct(Product product)
        {
            _productRepo.Add(product);
            _productRepo.Save();
        }
    }
}
