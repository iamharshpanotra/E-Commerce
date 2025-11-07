using ECommerceAPI.Core.Entities;
using ECommerceAPI.Core.Interfaces;
using ECommerceAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ECommerceAPI.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Product> GetProducts(string action, int? id = null)
        {
            var productList = new List<Product>();

            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = "usp_getProducts";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Action", action));
                command.Parameters.Add(new SqlParameter("@Id", id ?? (object)DBNull.Value));

                _context.Database.Connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    productList.Add(new Product
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = (decimal)reader["Price"],
                        Stock = (int)reader["Stock"],
                        CategoryId = (int)reader["CategoryId"],
                        IsDeleted = (bool)reader["IsDeleted"]
                    });
                }
                _context.Database.Connection.Close();
            }
            return productList;
        }

        public void ManageProduct(string action, Product product)
        {
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = "usp_manageProduct";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Action", action));
                command.Parameters.Add(new SqlParameter("@Id", product.Id));
                command.Parameters.Add(new SqlParameter("@Name", product.Name));
                command.Parameters.Add(new SqlParameter("@Description", product.Description));
                command.Parameters.Add(new SqlParameter("@Price", product.Price));
                command.Parameters.Add(new SqlParameter("@Stock", product.Stock));
                command.Parameters.Add(new SqlParameter("@CategoryId", product.CategoryId));
                command.Parameters.Add(new SqlParameter("@IsActive", product.IsActive));
                command.Parameters.Add(new SqlParameter("@IsDeleted", product.IsDeleted));


                _context.Database.Connection.Open();
                command.ExecuteNonQuery();
                _context.Database.Connection.Close();
            }
        }
    }
}
