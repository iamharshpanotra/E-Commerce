using ECommerceAPI.Core.DTOs;
using ECommerceAPI.Core.Entities;
using ECommerceAPI.Core.Interfaces.Service;
using System.Collections.Generic;
using System.Web.Http;
using System.Net;

namespace ECommerceAPI.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // Get all products
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var products = _productService.GetAll();
            return Ok(new ApiResponse<IEnumerable<ProductDto>>
            {
                Success = true,
                Message = "All products retrieved successfully.",
                Data = products,
                StatusCode = 200
            });
        }

        // Get Active & not deleted products
        [HttpGet]
        [Route("active")]
        public IHttpActionResult GetActive()
        {
            var products = _productService.GetActive();
            return Ok(new ApiResponse<IEnumerable<ProductDto>>
            {
                Success = true,
                Message = "Active products retrieved successfully.",
                Data = products,
                StatusCode = 200
            });
        }

        // Get product by ID
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return Content(System.Net.HttpStatusCode.NotFound,
                    new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Product not found.",
                        Data = null,
                        StatusCode = 404
                    });
            }

            return Ok(new ApiResponse<ProductDto>
            {
                Success = true,
                Message = "Product retrieved successfully.",
                Data = product,
                StatusCode = 200
            });
        }

        // Add new product
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(Product product)
        {
            _productService.Add(product);
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Product created successfully.",
                StatusCode = 201
            });
        }

        // Update product
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return Content(HttpStatusCode.BadRequest, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Product ID does not match.",
                    StatusCode = 400
                });
            }

            _productService.Update(product);

            return Content(HttpStatusCode.OK, new ApiResponse<string>
            {
                Success = true,
                Message = "Product updated successfully.",
                StatusCode = 200
            });
        }

        // Soft delete (IsDeleted = true)
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult SoftDelete(int id)
        {
            _productService.SoftDelete(id);
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Product soft-deleted successfully.",
                StatusCode = 200
            });
        }
    }
}
