using ECommerceAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories(string action, int? id = null);
        void ManageCategory(string action, Category category);
    }
}
