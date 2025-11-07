using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Core.DTOs
{
    public class CategoryRequestDto
    {
        [Required(ErrorMessage = "Category name is required.")]
        [MinLength(2, ErrorMessage = "Category name must be at least 2 characters.")]
        public string Name { get; set; }
    }
}
