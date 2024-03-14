
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        public virtual TblUserRole? Role { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<TblBrand> TblBrands { get; set; }
        [JsonIgnore]
        public virtual ICollection<TblCategory> TblCategories { get; set; }
        [JsonIgnore]
        public virtual ICollection<TblInventory> TblInventories { get; set; }
        [JsonIgnore]
        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
