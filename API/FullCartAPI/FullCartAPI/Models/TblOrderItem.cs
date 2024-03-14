using System;
using System.Collections.Generic;

namespace FullCartAPI.Models
{
    public partial class TblOrderItem
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? InventoryItemId { get; set; }
        public int? Quantity { get; set; }

        public virtual TblInventory? InventoryItem { get; set; }
        public virtual TblOrder? Order { get; set; }
    }
}
