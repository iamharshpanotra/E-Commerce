using ECommerceAPI.Core.Entities;
using ECommerceAPI.Core.Interfaces;
using ECommerceAPI.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceAPI.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Product> GetAll() => _repo.GetProducts("All");

        public IEnumerable<Product> GetActive() => _repo.GetProducts("Active");

        public Product GetById(int id) =>
            _repo.GetProducts("ById", id).FirstOrDefault();

        public void Add(Product product)
        {
            product.CreatedBy = "Admin";
            product.CreatedDate = DateTime.UtcNow;
            _repo.ManageProduct("Insert", product);
        }

        public void Update(Product product)
        {
            var existing = _repo.GetProducts("ById", product.Id).FirstOrDefault();
            if (existing == null)
                throw new Exception("Product not found.");

            // Only update the fields that were provided
            if (product.Name != null)
                existing.Name = product.Name;

            if (product.Description != null)
                existing.Description = product.Description;

            if (product.Price != 0)
                existing.Price = product.Price;

            if (product.Stock != 0)
                existing.Stock = product.Stock;

            if (product.CategoryId != 0)
                existing.CategoryId = product.CategoryId;

            existing.IsActive = product.IsActive; // boolean safe default
            existing.UpdatedBy = "Admin";
            existing.UpdatedDate = DateTime.UtcNow;

            _repo.ManageProduct("Update", existing);
        }

        public void SoftDelete(int id)
        {
            var product = new Product { Id = id };
            _repo.ManageProduct("Delete", product);
        }
    }
}
