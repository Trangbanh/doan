using Fruit.Base.Models;
using FruitKha.Base;
using FruitKha.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace FruitKha.Controllers
{
    public class ContactController : BaseAuthController<ContactController>
    {
        public ContactController(ILogger<ContactController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult SendContact(Contacts data)
        {
            try
            {
                if(string.IsNullOrEmpty(data.Email) || string.IsNullOrEmpty(data.Name) || string.IsNullOrEmpty(data.Phone) || string.IsNullOrEmpty(data.Message))
                {
                    return Error("Vui lòng nhập đầy đủ thông tin liên hệ");
                }
                data.CreatedDate = DateTime.Now;
                _base.contacts.Insert(data);
                _base.Commit();
                return Success("Gửi liên hệ thành công");
            }
            catch (Exception ex)
            {
                return Error("Đã xảy ra lỗi. Vui lòng thử lại sau");
            }
        }
    }
}
