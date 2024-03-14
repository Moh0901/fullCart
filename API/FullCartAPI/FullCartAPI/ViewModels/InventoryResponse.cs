namespace FullCartAPI.ViewModels
{
    public class InventoryResponse
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int? Quantity { get; set; }
        public string? ImagePath { get; set; }
        public int? UserId { get; set; }
        //public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Status { get; set; }
    }
}
