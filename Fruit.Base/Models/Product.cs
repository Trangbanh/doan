using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fruit.Base.Models
{
    public partial class Product
    {
        public Product()
        {
            DetailedInvoices = new HashSet<DetailedInvoice>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal PromotionalPrice { get; set; }
        public string? Image { get; set; }
        public int Idtype { get; set; }
        public virtual ProductType? IdtypeNavigation { get; set; }
        public virtual ICollection<DetailedInvoice> DetailedInvoices { get; set; }
        [NotMapped]
        public int TotalRow { get; set; }
        [NotMapped]
        public string? TypeName { get; set; }
    }
}
