
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FullCartAPI.Models
{
    public partial class TblUserRole
    {
        public TblUserRole()
        {
            TblUsers = new HashSet<TblUser>();
        }

        public int Id { get; set; }
        public string? Role { get; set; }

        [JsonIgnore]
        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
