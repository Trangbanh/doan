using Fruit.Base.Models;
using FruitKha.Areas.Admin.Controllers.Base;
using FruitKha.Base;
using Microsoft.AspNetCore.Mvc;

namespace FruitKha.Areas.Admin.Controllers
{
    public class BillController : BaseController<BillController>
    {
        public BillController(ILogger<BillController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetBillData(string search, int offset, int limit)
        {
            int total = 0;
            var data = _base.bills.GetBillListAllPaging(search, offset, limit);
            if (data != null && data.Count > 0)
            {
                total = data[0].TotalRow;
            }
            return Data(new { rows = data, total = total });
        }
        public JsonResult ChangeStatus(int Id)
        {
            try
            {
                var entity = _base.bills.GetByID(Id);
                if (entity == null)
                {
                    return Error("Thông tin đơn hàng không tồn tại");
                }
                entity.Status = (entity.Status ? false : true);
                _base.bills.Update(entity);
                _base.Commit();
                return Success("Duyệt đơn hàng thành công");
            }
            catch (Exception ex)
            {
                return Error("Lỗi " + ex);
            }
        }
        public IActionResult BillDetail(int Id)
        {
            Bill model = new Bill();
            var data = _base.bills.GetByID(Id);
            if(data != null)
            {
                model = data;
            }
            return View(model);
        }
        public JsonResult GetBillDetail(int billId, int offset, int limit)
        {
            int total = 0;
            decimal totalPriceAll = 0;
            var data = _base.bills.GetDetailedInvoicesByIdBill(billId, offset, limit);  
            if(data != null && data.Count > 0)
            {
                total = data[0].TotalRow;
                totalPriceAll = data[0].TotalPriceAll;
            }
            return Data(new { rows = data, total = total, totalPriceAll = totalPriceAll });
        }
    }
}
