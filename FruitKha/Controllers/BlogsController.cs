using Fruit.Base.Models;
using FruitKha.Base;
using FruitKha.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace FruitKha.Controllers
{
    public class BlogsController : BaseAuthController<BlogsController>
    {
        public BlogsController(ILogger<BlogsController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {
        }
        public IActionResult Index(int? page)
        {
            int total = 0;
            int pageSize = 6;
            int pageNumber = page ?? 1;
            int startIndex = (pageNumber - 1) * pageSize;
            List<Blog> model = _base.blogs.GetBlogListAllPaging(null, startIndex, pageSize);
            if (model != null && model.Count > 0)
            {
                total = model[0].TotalRow;
            }
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)total / pageSize);
            return View(model);
        }
        public IActionResult Detail(int Id)
        {
            Blog model = new Blog();
            List<Comment> lstComment = new List<Comment>();
            var entity = _base.blogs.GetByID(Id);
            if(entity != null)
            {
                model = entity;
				lstComment = _base.comments.GetCommentListAllPaging(null, 0, 9999999).Where(x => x.Idblog == entity.Idblog).ToList();
			}
            ViewBag.ListComment = lstComment;

			return View(model);
        }
        public JsonResult SendComment(string comment, int idBlog)
        {
            try
            {
                var user = GetUser();
                if (string.IsNullOrEmpty(comment))
                {
                    return Error("Vui lòng nhập bình luận");
                }
                var obj = new Comment
                {
                    Idcommentator = user.CustomerId,
                    CommentContent = comment,
                    Idblog = idBlog,
                    Date = DateTime.Now.Date,
                };
                _base.comments.Insert(obj);
                _base.Commit();
                return Success("Thêm mới bình luận thành công");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}
