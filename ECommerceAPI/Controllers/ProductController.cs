using ECommerceAPI.Application.Services;
using ECommerceAPI.Core.Entities;
using ECommerceAPI.Core.Interfaces.Service;
using ECommerceAPI.Infrastructure.Data;
using ECommerceAPI.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace ECommerceAPI.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController()
        {
            var dbContext = new ApplicationDbContext();
            _productService = new ProductService(new ProductRepository(dbContext));
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll() => Ok(_productService.GetAll());

        [HttpGet]
        [Route("active")]
        public IHttpActionResult GetActive() => Ok(_productService.GetActive());

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            return product == null ? (IHttpActionResult)NotFound() : Ok(product);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(Product product)
        {
            _productService.Add(product);
            return Ok("Product created.");
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, Product product)
        {
            if (id != product.Id) return BadRequest("ID mismatch.");
            _productService.Update(product);
            return Ok("Product updated.");
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult SoftDelete(int id)
        {
            _productService.SoftDelete(id);
            return Ok("Product soft-deleted.");
        }
    }
}
