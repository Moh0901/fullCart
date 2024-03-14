namespace FullCartAPI.ViewModels
{
    public class BrandViewModel
    {
        public string? BrandName { get; set; }
        public IFormFile? ImagePath { get; set; }
        public int UserId { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
    }
}
