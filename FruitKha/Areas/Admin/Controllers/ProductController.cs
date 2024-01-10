using Fruit.Base.Models;
using FruitKha.Areas.Admin.Controllers.Base;
using FruitKha.Base;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace FruitKha.Areas.Admin.Controllers
{
    public class ProductController : BaseController<ProductController>
    {
        public ProductController(ILogger<ProductController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetProductData(string search, int offset, int limit)
        {
            int total = 0;
            var data = _base.products.GetProductListAllPaging(search, offset, limit);
            if (data != null && data.Count > 0)
            {
                total = data[0].TotalRow;
            }
            return Data(new { rows = data, total = total });
        }
        [HttpGet]
        public IActionResult CreateOrEdit(int Id)
        {
            ViewBag.ListProductType = _base.ProductType.Get().ToList();
            var model = new Product();
            var detail = _base.products.GetByID(Id);
            if (detail != null)
            {
                model = detail;
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateOrEdit(Product product, IFormFile fileUpload)
        {
            try
            {
                if (string.IsNullOrEmpty(product.ProductName))
                {
                    return Error("Vui lòng nhập tên sản phẩm");
                }
                if (product.Idtype == 0)
                {
                    return Error("Vui lòng chọn danh mục sản phẩm");
                }
                if (product.Price < 0 || product.Price < 0)
                {
                    return Error("Vui lòng nhập giá sản phẩm lớn hơn 0");
                }
                if(product.PromotionalPrice > product.Price)
                {
                    return Error("Giá giảm giá không thể lớn hơn giá gốc");
                }
                var pathDb = string.Empty;
                if (fileUpload != null)
                {
                    Regex rgx = new Regex(@"(.*?)\.(jpg|bmp|png|gif|jfif|JPG|PNG|BMP|GIF|JFIF)$");
                    if (!rgx.IsMatch(fileUpload.FileName))
                    {
                        return Error("Định dạnh ảnh không hợp lệ");
                    }
                    string filename = DateTime.Now.ToString("yyyyMMdd") + '_' + fileUpload.FileName;
                    string folderName = DateTime.Now.ToString("yyMMdd");
                    string newPath = Path.Combine(folderName);
                    string SavePath = Path.Combine("wwwroot/Uploads/Product", newPath, filename);
                    var fi = new FileInfo(SavePath);
                    if (!Directory.Exists(fi.DirectoryName))
                    {
                        Directory.CreateDirectory(fi.DirectoryName);
                    }
                    using (var stream = new FileStream(SavePath, FileMode.Create))
                    {
                        fileUpload.CopyTo(stream);
                    }
                    pathDb = folderName + "\\" + filename;
                }
                if (product.ProductId == 0)
                {
                    product.Image = pathDb;
                    _base.products.Insert(product);
                }
                else
                {
                    var entity = _base.products.GetByID(product.ProductId);
                    if (entity != null)
                    {
                        entity.ProductName = product.ProductName;
                        entity.Image = (!string.IsNullOrEmpty(pathDb) ? pathDb : entity.Image);
                        entity.Price = product.Price;
                        entity.PromotionalPrice = product.PromotionalPrice;
                        entity.Description = product.Description;
                        _base.products.Update(entity);
                    }
                    else
                    {
                        return Error("Thông tin sản phẩm không tồn tại");
                    }
                }
                _base.Commit();
                return Success("Cập nhật thông tin thành công");
            }
            catch (Exception ex)
            {
                return Error("Lỗi " + ex);
            }
        }
        public JsonResult Delete(int Id)
        {
            try
            {
                var entity = _base.products.GetByID(Id);
                if (entity == null)
                {
                    return Error("Thông tin sản phẩm không tồn tại");
                }
                _base.products.Delete(entity);
                _base.Commit();
                return Success("Xóa thành công");
            }
            catch (Exception ex)
            {
                return Error("Lỗi " + ex);
            }
        }
    }
}
