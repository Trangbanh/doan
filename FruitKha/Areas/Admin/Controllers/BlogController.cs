using Fruit.Base.Models;
using FruitKha.Areas.Admin.Controllers.Base;
using FruitKha.Base;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace FruitKha.Areas.Admin.Controllers
{
    public class BlogController : BaseController<BlogController>
    {
        public BlogController(ILogger<BlogController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetBlogData(string search, int offset, int limit)
        {
            int total = 0;
            var data = _base.blogs.GetBlogListAllPaging(search, offset, limit); 
            if(data != null && data.Count > 0)
            {
                total = data[0].TotalRow;
            }
            return Data(new { rows = data, total = total });
        }
        [HttpGet]
        public IActionResult CreateOrEdit(int Id)
        {
            var model = new Blog();
            var entity = _base.blogs.GetByID(Id);
            if (entity != null)
            {
                model = entity;
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult CreateOrEdit(Blog model, IFormFile fileUpload)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Title))
                {
                    return Error("Vui lòng nhập tiêu đề");
                }
                if (string.IsNullOrEmpty(model.ShortContent))
                {
                    return Error("Vui lòng nhập mô tả ngắn");
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
                    string SavePath = Path.Combine("wwwroot/Uploads/Blogs", newPath, filename);
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
                if (model.Idblog == 0)
                {
                    model.Image = pathDb;
                    model.CreatedDate = DateTime.Now;
                    _base.blogs.Insert(model);
                }
                else
                {
                    var entity = _base.blogs.GetByID(model.Idblog);
                    if (entity != null)
                    {
                        entity.Title = model.Title;
                        entity.ShortContent = model.ShortContent;
                        entity.Image = (!string.IsNullOrEmpty(pathDb) ? pathDb : entity.Image);
                        entity.Contents = model.Contents;
                        entity.ModifyDate = DateTime.Now;   
                        _base.blogs.Update(entity);
                    }
                    else
                    {
                        return Error("Thông tin bài viết không tồn tại");
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
                var entity = _base.blogs.GetByID(Id);
                if (entity == null)
                {
                    return Error("Thông tin bài viết không tồn tại");
                }
                _base.blogs.Delete(entity);
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
