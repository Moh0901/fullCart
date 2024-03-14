using System;
using System.Collections.Generic;

namespace FullCartAPI.Models
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblBrands = new HashSet<TblBrand>();
            TblCategories = new HashSet<TblCategory>();
            TblInventories = new HashSet<TblInventory>();
            TblOrders = new HashSet<TblOrder>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }

        public virtual TblUserRole Role { get; set; } = null!;
        public virtual ICollection<TblBrand> TblBrands { get; set; }
        public virtual ICollection<TblCategory> TblCategories { get; set; }
        public virtual ICollection<TblInventory> TblInventories { get; set; }
        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
