using ECommerceAPI.Core.DTOs;
using ECommerceAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Interfaces.Service
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();
        IEnumerable<CategoryDto> GetActive();
        CategoryDto GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void SoftDelete(int id);
    }
}
