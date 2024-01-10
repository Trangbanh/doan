using BaseRepo.Repositories;
using Dapper;
using Fruit.Base.Interface;
using Fruit.Base.Models;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fruit.Base.Implement
{
	public class IplBill : Repository<Bill>, IBill
    {
        public IConfiguration _configuration { get; set; }
        internal string _cnnString = string.Empty;
        public fruitkhaContext _dbContext;
        public IplBill(fruitkhaContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DefaultConnection");
        }

        public List<Bill> GetBillListAllPaging(string search, int offset, int limit)
        {
            var list = new List<Bill>();
            var unitOfWork = new UnitOfWorkFactory(_cnnString);
            try
            {
                using (var u = unitOfWork.Create(false))
                {
                    var p = new DynamicParameters();
                    p.Add("@search", search);
                    p.Add("@offset", offset);
                    p.Add("@limit", limit);
                    list = u.GetIEnumerable<Bill>("Sp_GetBillListAllPaging", p).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return list;
        }

        public List<DetailedInvoice> GetDetailedInvoicesByIdBill(int billId, int offset, int limit)
        {
            var list = new List<DetailedInvoice>();
            var unitOfWork = new UnitOfWorkFactory(_cnnString);
            try
            {
                using (var u = unitOfWork.Create(false))
                {
                    var p = new DynamicParameters();
                    p.Add("@IdBill", billId);
                    p.Add("@offset", offset);
                    p.Add("@limit", limit);
                    list = u.GetIEnumerable<DetailedInvoice>("Sp_GetDetailInvoiceByBillIdListAll", p).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return list;
        }

		public int InserOrder(Bill bill)
		{
			var unitOfWork = new UnitOfWorkFactory(_cnnString);
			try
			{
				using (var u = unitOfWork.Create(true))
				{
					var p = new DynamicParameters();
					p.Add("@OrderCode", bill.OrderCode);
					p.Add("@UserId", bill.UseId);
					p.Add("@Status", bill.Status);
					p.Add("@PhoneNumber", bill.Phone);
					p.Add("@Address", bill.Address);
					p.Add("@NewId", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
					var result = u.ProcedureExecute("Sp_InsertBill", p);
					if (result > 0)
					{
						return p.Get<int>("@NewId");
					}
					return 0;
				}
			}
			catch (Exception ex)
			{
				return 0;
			}
		}

		public List<ReportBill> GetReportBills()
		{
			var list = new List<ReportBill>();
			var unitOfWork = new UnitOfWorkFactory(_cnnString);
			try
			{
				using (var u = unitOfWork.Create(false))
				{
					list = u.GetIEnumerable<ReportBill>("Sp_ReportHome").ToList();
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			return list;
		}
	}
}
