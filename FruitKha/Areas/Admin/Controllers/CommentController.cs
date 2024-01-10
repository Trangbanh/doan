using FruitKha.Areas.Admin.Controllers.Base;
using FruitKha.Base;
using Microsoft.AspNetCore.Mvc;

namespace FruitKha.Areas.Admin.Controllers
{
    public class CommentController : BaseController<CommentController>
    {
        public CommentController(ILogger<CommentController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetCommentData(string search, int offset, int limit)
        {
            int total = 0;
            var data = _base.comments.GetCommentListAllPaging(search, offset, limit);
            if(data != null && data.Count > 0)
            {
                total = data[0].TotalRow;
            }
            return Data(new { rows = data, total = total });
        }
        public JsonResult Delete(int Id)
        {
            try
            {
                var entity = _base.comments.GetByID(Id);
                if(entity == null)
                {
                    return Error("Thông tin bình luận không tồn tại");
                }
                _base.comments.Delete(entity);
                _base.Commit();
                return Success("Xóa thành công");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}
