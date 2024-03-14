using System;
using System.Collections.Generic;

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
        public virtual ICollection<TblOrderItem> TblOrderItems { get; set; }
    }
}
