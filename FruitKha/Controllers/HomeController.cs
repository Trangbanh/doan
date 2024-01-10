using FruitKha.Base;
using FruitKha.Controllers.Base;
using FruitKha.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FruitKha.Controllers
{
    public class HomeController : BaseAuthController<HomeController>
	{
		public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
		{
		}
		public IActionResult Index()
        {
            var model = new HomeModel();    
            model.ListOurProduct = _base.products.Get().Take(6).OrderByDescending(x => x.ProductId).ToList();
            model.ListBlogs = _base.blogs.Get().Take(6).OrderByDescending(x => x.CreatedDate).ToList();
            model.ListFeaturedCate = _base.ProductType.Get(x => x.IsHome == true).Take(6).ToList();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
