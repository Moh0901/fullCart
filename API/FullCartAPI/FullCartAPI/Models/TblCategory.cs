using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FullCartAPI.Models
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            TblInventories = new HashSet<TblInventory>();
        }

        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public string? ImagePath { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual TblUser User { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<TblInventory> TblInventories { get; set; }
    }
}
