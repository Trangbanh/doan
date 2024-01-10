using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fruit.Base.Models
{
    public partial class Bill
    {
        public Bill()
        {
            DetailedInvoices = new HashSet<DetailedInvoice>();
        }

        public int Idbill { get; set; }
        public string OrderCode { get; set; }
        public int UseId { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool Status { get; set; }
        public DateTime? OrderDate { get; set; }
        public virtual User? Use { get; set; }
        public virtual ICollection<DetailedInvoice> DetailedInvoices { get; set; }
        [NotMapped]
        public int TotalRow { get; set; }
        [NotMapped]
        public string UseName { get; set; }
        [NotMapped]
        public string FullName { get; set; }
    }
    public class ReportBill
    {
        public int MonthNumber { get; set; }
        public decimal TotalPriceMonth { get; set; }
	}
}
