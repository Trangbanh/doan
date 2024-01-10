using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fruit.Base.Models
{
    public partial class User
    {
        public User()
        {
            Bills = new HashSet<Bill>();
        }

        public int UseId { get; set; }
        public string? Name { get; set; }
        public string? Sdt { get; set; }
        public string? UseName { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }      
        public virtual ICollection<Bill> Bills { get; set; }
        [NotMapped]
        public int TotalRow { get; set; }
    }
}
