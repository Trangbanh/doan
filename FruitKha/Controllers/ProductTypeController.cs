using Fruit.Base.Models;
using FruitKha.Base;
using FruitKha.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace FruitKha.Controllers
{
    public class ProductTypeController : BaseAuthController<ProductTypeController>
    {
        public ProductTypeController(ILogger<ProductTypeController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {
        }
        public IActionResult Index(int? page)
        {
            int total = 0;
            int pageSize = 6;
            int pageNumber = page ?? 1;
            int startIndex = (pageNumber - 1) * pageSize;
            List<ProductType> model = _base.ProductType.GetProductTypeListAll(null, startIndex, pageSize);
            if(model != null && model.Count > 0)
            {
                total = model[0].TotalRow;
            }
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)total / pageSize);
            return View(model);
        }
        public IActionResult Detail(int Id, int? page)
        {
            var typeName = string.Empty;
            var entity = _base.ProductType.GetByID(Id);
            if(entity != null)
            {
                typeName = entity.TypeName;
            }
            ViewBag.TypeName = typeName;
            ViewBag.TypeId = Id;
            int total = 0;
            int pageSize = 6;
            int pageNumber = page ?? 1;
            int startIndex = (pageNumber - 1) * pageSize;
            List<Product> model = _base.products.GetProductListAllPaging(null, startIndex, pageSize,Id);
            if (model != null && model.Count > 0)
            {
                total = model[0].TotalRow;
            }
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)total / pageSize);
            return View(model);
        }
    }
}
