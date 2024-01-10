using BaseRepo.Interfaces;
using Fruit.Base.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fruit.Base.Interface
{
    public interface IBill : IRepository<Bill>
    {
        List<Bill> GetBillListAllPaging(string search, int offset, int limit);
        List<DetailedInvoice> GetDetailedInvoicesByIdBill(int billId, int offset, int limit);
		int InserOrder(Bill bill);
        List<ReportBill> GetReportBills();
	}
}
