namespace FullCartAPI.ViewModels
{
    public class CategoryResponse
    {
        public string? CategoryName { get; set; }
        public string? ImagePath { get; set; }
        public int UserId { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
