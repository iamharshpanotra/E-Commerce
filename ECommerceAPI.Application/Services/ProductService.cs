using System;
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
            product.CreatedBy = "Admin";
            product.CreatedDate = DateTime.UtcNow;
            _productRepo.Add(product);
            _productRepo.Save();
        }


        public void UpdateProduct(Product updatedProduct)
        {
            var existingProduct = _productRepo.GetById(updatedProduct.Id);

            if (existingProduct == null)
                throw new Exception("Product not found");

            // Update only the fields that are changeable
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Stock = updatedProduct.Stock;
            existingProduct.CategoryId = updatedProduct.CategoryId;

            // Audit fields
            existingProduct.UpdatedBy = "Admin"; // later replace with actual user
            existingProduct.UpdatedDate = DateTime.UtcNow;

            _productRepo.Update(existingProduct);
            _productRepo.Save();
        }


        public void DeleteProduct(int id)
        {
            var product = _productRepo.GetById(id);
            product.IsDeleted = true;
            product.DeletedBy = "Admin";
            product.DeletedDate = DateTime.UtcNow;
            _productRepo.Update(product);
            _productRepo.Save();
        }

    }
}
