using ECommerceAPI.Application.Services;
using ECommerceAPI.Core.Entities;
using ECommerceAPI.Core.Interfaces;
using ECommerceAPI.Infrastructure.Data;
using ECommerceAPI.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ECommerceAPI.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductController : ApiController
    {
        private readonly ProductService _productService;

        public ProductController()
        {
            // Temporary manual dependency setup
            var dbContext = new ApplicationDbContext();
            var productRepo = new GenericRepository<Product>(dbContext);
            _productService = new ProductService(productRepo);
        }

        // GET: api/products
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetProduct(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _productService.AddProduct(product);
            return Ok("Product created successfully.");
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != product.Id)
                return BadRequest("Product ID mismatch");

            try
            {
                _productService.UpdateProduct(product);
                return Ok("Product updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
