using FruitKha.Areas.Admin.Controllers.Base;
using FruitKha.Base;
using Microsoft.AspNetCore.Mvc;

namespace FruitKha.Areas.Admin.Controllers
{
  
    public class HomeController : BaseController<HomeController>
    {
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetChartReportBill()
        {
            var data = _base.bills.GetReportBills();
            return new JsonResult(new
            {
                lstMonth = data.Select(x => x.MonthNumber).ToList(),
                lstPrice = data.Select(x => x.TotalPriceMonth).ToList(),
            });
        }
    }
}
