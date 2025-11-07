using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceAPI.Core.DTOs;
using ECommerceAPI.Core.Entities;
using ECommerceAPI.Core.Interfaces;
using ECommerceAPI.Core.Interfaces.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public IEnumerable<ProductDto> GetAll()
    {
        return _repo.GetProducts("All").Select(MapToDto);
    }


    public IEnumerable<ProductDto> GetActive()
    {
        return _repo.GetProducts("Active").Select(MapToDto);
    }

    public ProductDto GetById(int id)
    {
        var product = _repo.GetProducts("ById", id).FirstOrDefault();
        return product == null ? null : MapToDto(product);
    }

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

        // Conditional updates — only overwrite when non-default or explicitly set
        if (!string.IsNullOrEmpty(product.Name))
            existing.Name = product.Name;

        if (!string.IsNullOrEmpty(product.Description))
            existing.Description = product.Description;

        if (product.Price != default)
            existing.Price = product.Price;

        if (product.Stock != default)
            existing.Stock = product.Stock;

        if (product.CategoryId != default)
            existing.CategoryId = product.CategoryId;

        // ✅ Always take IsActive and IsDeleted even if false
        existing.IsActive = product.IsActive;
        existing.IsDeleted = product.IsDeleted;

        existing.UpdatedBy = "Admin";
        existing.UpdatedDate = DateTime.UtcNow;

        _repo.ManageProduct("Update", existing);
    }

    public void SoftDelete(int id)
    {
        var product = new Product { Id = id };
        _repo.ManageProduct("Delete", product);
    }

    // Mapping Function
    private ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name
        };
    }
}
