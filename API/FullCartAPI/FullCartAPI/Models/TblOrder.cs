using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FullCartAPI.Models
{
    public partial class TblOrder
    {
        public TblOrder()
        {
            TblOrderItems = new HashSet<TblOrderItem>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Status { get; set; }

        public virtual TblUser User { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<TblOrderItem> TblOrderItems { get; set; }
    }
}
