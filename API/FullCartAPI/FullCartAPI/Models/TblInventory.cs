using System;
using System.Collections.Generic;

namespace FullCartAPI.Models
{
    public partial class TblInventory
    {
        public TblInventory()
        {
            TblOrderItems = new HashSet<TblOrderItem>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int? Quantity { get; set; }
        public string? ImagePath { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Status { get; set; }

        public virtual TblBrand? Brand { get; set; }
        public virtual TblCategory? Category { get; set; }
        public virtual TblUser? User { get; set; }
        public virtual ICollection<TblOrderItem> TblOrderItems { get; set; }
    }
}
