using ECommerceAPI.Core.Entities;
using ECommerceAPI.Core.Interfaces.Repository;
using ECommerceAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Category> GetCategories(string action, int? id = null)
        {
            var categories = new List<Category>();

            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = "usp_getCategories";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Action", action));
                command.Parameters.Add(new SqlParameter("@Id", id ?? (object)DBNull.Value));

                _context.Database.Connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        IsActive = (bool)reader["IsActive"],
                        IsDeleted = (bool)reader["IsDeleted"]
                    });
                }
                _context.Database.Connection.Close();
            }
            return categories;
        }

        public void ManageCategory(string action, Category category)
        {
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = "usp_manageCategory";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Action", action));
                command.Parameters.Add(new SqlParameter("@Id", category.Id));
                command.Parameters.Add(new SqlParameter("@Name", category.Name));
                command.Parameters.Add(new SqlParameter("@IsActive", category.IsActive));
                command.Parameters.Add(new SqlParameter("@IsDeleted", category.IsDeleted));

                _context.Database.Connection.Open();
                command.ExecuteNonQuery();
                _context.Database.Connection.Close();
            }
        }
    }
}
