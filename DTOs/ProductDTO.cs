using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = null!;

        public int Price { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string? Description { get; set; }

        public string ImageUrl { get; set; } = null!;

    }
}
