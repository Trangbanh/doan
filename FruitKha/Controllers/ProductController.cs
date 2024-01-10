using Fruit.Base.Models;
using FruitKha.Base;
using FruitKha.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace FruitKha.Controllers
{
    public class ProductController : BaseAuthController<ProductController>
    {
        public ProductController(ILogger<ProductController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {
        }
        public IActionResult Index(int? page)
        {
            int total = 0;
            int pageSize = 6;
            int pageNumber = page ?? 1;
            int startIndex = (pageNumber - 1) * pageSize;
            List<Product> model = _base.products.GetProductListAllPaging(null, startIndex, pageSize); 
            if(model != null && model.Count > 0)
            {
                total = model[0].TotalRow;
            }
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)total / pageSize);
            return View(model);
        }
        public IActionResult Detail(int Id)
        {
            Product model = new Product();
            List<Product> lstRelatedPro = new List<Product>();
			var entity = _base.products.GetByID(Id);
            if(entity != null)
            {
                lstRelatedPro = _base.products.Get(x => x.Idtype == entity.Idtype && x.ProductId != entity.ProductId).Take(3).ToList();
                model = entity;
            }
            ViewBag.LstRelatedPro = lstRelatedPro;
			return View(model);
        }
        public IActionResult Search(string search, int? page)
        {
            ViewBag.Search = search;    
            int total = 0;
            int pageSize = 6;
            int pageNumber = page ?? 1;
            int startIndex = (pageNumber - 1) * pageSize;
            List<Product> model = _base.products.GetProductListAllPaging(search, startIndex, pageSize);
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
