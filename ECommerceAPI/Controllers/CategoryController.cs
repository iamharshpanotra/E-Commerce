using ECommerceAPI.Core.DTOs;
using ECommerceAPI.Core.Entities;
using ECommerceAPI.Core.Interfaces.Service;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace ECommerceAPI.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var categories = _categoryService.GetAll();

            return Ok(new ApiResponse<IEnumerable<CategoryDto>>
            {
                Success = true,
                Message = "All categories fetched successfully.",
                Data = categories,
                StatusCode = 200
            });
        }

        [HttpGet]
        [Route("active")]
        public IHttpActionResult GetActive() =>
            Ok(new ApiResponse<IEnumerable<CategoryDto>>
            {
                Success = true,
                Message = "Active categories fetched successfully.",
                Data = _categoryService.GetActive(),
                StatusCode = 200
            });

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return Content(System.Net.HttpStatusCode.NotFound,
                    new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Category not found.",
                        StatusCode = 404
                    });
            }

            return Ok(new ApiResponse<CategoryDto>
            {
                Success = true,
                Message = "Category retrieved successfully.",
                Data = category,
                StatusCode = 200
            });
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(Category category)
        {
            _categoryService.Add(category);
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Category created successfully.",
                StatusCode = 201
            });
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, Category category)
        {
            if (id != category.Id)
            {
                return Content(HttpStatusCode.BadRequest, new ApiResponse<string>
                {
                    Success = false,
                    Message = "ID mismatch.",
                    StatusCode = 400
                });
            }

            _categoryService.Update(category);
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Category updated successfully.",
                StatusCode = 200
            });
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult SoftDelete(int id)
        {
            _categoryService.SoftDelete(id);
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Category soft-deleted successfully.",
                StatusCode = 200
            });
        }
    }
}