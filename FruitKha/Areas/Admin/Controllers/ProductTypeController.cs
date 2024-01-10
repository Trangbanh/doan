using Fruit.Base.Models;
using FruitKha.Areas.Admin.Controllers.Base;
using FruitKha.Base;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace FruitKha.Areas.Admin.Controllers
{
    public class ProductTypeController : BaseController<ProductTypeController>
    {
        public ProductTypeController(ILogger<ProductTypeController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetProductTypeData(string search, int offset, int limit)
        {
            int total = 0;
            var data = _base.ProductType.GetProductTypeListAll(search, offset, limit);
            if(data != null && data.Count > 0)
            {
                total = data[0].TotalRow;
            }
            return Data(new { rows = data, total = total });
        }
        public JsonResult ChangeIsHome(int Id)
        {
            try
            {
                var entity = _base.ProductType.GetByID(Id);
                if (entity == null)
                {
                    return Error("Thông tin danh mục không tồn tại");
                }
                entity.IsHome = (entity.IsHome ? false : true);
                _base.ProductType.Update(entity);
                _base.Commit();
                return Success("Thay đổi trạng thái thành công");
            }
            catch (Exception ex)
            {
                return Error("Lỗi " + ex);
            }
        }
        public JsonResult CreateOrEdit(ProductType model, IFormFile fileUpload)
        {
            try
            {
                if (string.IsNullOrEmpty(model.TypeName))
                {
                    return Error("Vui lòng nhập tên danh mục");
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
                    string SavePath = Path.Combine("wwwroot/Uploads/Cates", newPath, filename);
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
                if(model.Idtype == 0)
                {
                    model.Images = pathDb;
                    _base.ProductType.Insert(model);
                }
                else
                {
                    var entity = _base.ProductType.GetByID(model.Idtype);
                    if(entity == null)
                    {
                        return Error("Thông tin danh mục không tồn tại");
                    }
                    entity.TypeName = model.TypeName;
                    entity.Images = (!string.IsNullOrEmpty(pathDb) ? pathDb : entity.Images);
                    _base.ProductType.Update(entity);
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
                var entity = _base.ProductType.GetByID(Id);
                if (entity == null)
                {
                    return Error("Thông tin danh mục không tồn tại");
                }
                var product = _base.products.Read(x => x.Idtype == entity.Idtype);
                if(product != null)
                {
                    return Error("Danh mục " + entity.TypeName + " đang có sản phẩm. Không thể xóa được");
                }
                _base.ProductType.Delete(entity);
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
