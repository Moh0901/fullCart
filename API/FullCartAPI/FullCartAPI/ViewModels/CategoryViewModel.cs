using System.ComponentModel.DataAnnotations.Schema;

namespace FullCartAPI.ViewModels
{
    public class CategoryViewModel
    {
        public string? CategoryName { get; set; }

        [NotMapped]
        public IFormFile? ImagePath { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
