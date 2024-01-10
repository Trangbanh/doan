using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fruit.Base.Models
{
    public partial class DetailedInvoice
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int Idbill { get; set; }
        public virtual Bill? IdbillNavigation { get; set; }
        public virtual Product? Product { get; set; }
        [NotMapped]
        public int TotalRow { get; set; }
        [NotMapped]
        public decimal TotalPriceAll { get; set; }
        [NotMapped]
        public string? ProductName { get; set; }  
    }
}
