using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fruit.Base.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        public int Idtype { get; set; }
        public string? TypeName { get; set; }
        public string? Images { get; set; }
        public bool IsHome { get; set; }
        [NotMapped]
        public int TotalRow { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
