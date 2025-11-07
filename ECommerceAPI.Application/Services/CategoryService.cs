using ECommerceAPI.Core.DTOs;
using ECommerceAPI.Core.Entities;
using ECommerceAPI.Core.Interfaces.Repository;
using ECommerceAPI.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _repo.GetCategories("All").Select(MapToDto);
        }

        public IEnumerable<CategoryDto> GetActive()
        {
            return _repo.GetCategories("Active").Select(MapToDto);
        }

        public CategoryDto GetById(int id)
        {
            var category = _repo.GetCategories("ById", id).FirstOrDefault();
            return category == null ? null : MapToDto(category);
        }

        public void Add(Category category)
        {
            category.CreatedBy = "Admin";
            category.CreatedDate = DateTime.UtcNow;
            _repo.ManageCategory("Insert", category);
        }

        public void Update(Category category)
        {
            var existing = _repo.GetCategories("ById", category.Id).FirstOrDefault();
            if (existing == null)
                throw new Exception("Category not found.");

            existing.Name = category.Name;
            existing.IsActive = category.IsActive;
            existing.UpdatedBy = "Admin";
            existing.UpdatedDate = DateTime.UtcNow;

            _repo.ManageCategory("Update", existing);
        }

        public void SoftDelete(int id)
        {
            var category = new Category { Id = id, IsDeleted = true, DeletedBy = "Admin", DeletedDate = DateTime.UtcNow };
            _repo.ManageCategory("Delete", category);
        }

        private CategoryDto MapToDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
