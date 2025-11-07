using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Core.DTOs
{
    public class ProductRequestDto
    {
        [Required(ErrorMessage = "Product name is required.")]
        [MinLength(2, ErrorMessage = "Product name must be at least 2 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "CategoryId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid CategoryId.")]
        public int CategoryId { get; set; }
    }
}
