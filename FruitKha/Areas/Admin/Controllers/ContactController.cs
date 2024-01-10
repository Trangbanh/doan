using Fruit.Base.Models;
using FruitKha.Areas.Admin.Controllers.Base;
using FruitKha.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace FruitKha.Areas.Admin.Controllers
{
    public class ContactController : BaseController<ContactController>
    {
        public ContactController(ILogger<ContactController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData(string search, int offset, int limit)
        {
            int total = 0;
            var data = _base.contacts.GetContactListAllPaging(search, offset, limit);   
            if(data != null && data.Count > 0)
            {
                total = data[0].TotalRow;
            }
            return Data(new { total = total, rows = data });
        }
        public JsonResult RepContact(Contacts contacts)
        {
            try
            {
                if (string.IsNullOrEmpty(contacts.Email))
                {
                    return Error("Không tìm thấy email cần gửi");
                }
                if (string.IsNullOrEmpty(contacts.Message))
                {
                    return Error("Vui lòng nhập nội dung khách hàng");
                }
                if (string.IsNullOrEmpty(contacts.RepMessage))
                {
                    return Error("Vui lòng nhập nội dung phản hồi");
                }
                var entity = _base.contacts.GetByID(contacts.ContactId);
                if(entity == null)
                {
                    return Error("Không tìm thấy thông tin liên hệ trong hệ thống");
                }
                bool flag = SendMail(contacts.Email, contacts.Message, contacts.RepMessage);
                if (!flag)
                {
                    return Error("Không thể gửi mail cho khách hàng");
                }
                entity.IsRead = 1;
                _base.contacts.Update(entity);
                _base.Commit();
                return Success("Gửi phản hồi thành công");
            }
            catch (Exception ex)
            {
                return Error("Lỗi " + ex.Message);
            }
        }
        public bool SendMail(string emailTo, string message, string repMessage)
        {
            try
            {
                var servername = _configuration.GetSection("servername");
                var emailaddress = _configuration.GetSection("email");
                var port = _configuration.GetSection("port");
                var password = _configuration.GetSection("password");
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(servername.Value);
                mail.From = new MailAddress(emailaddress.Value);
                mail.IsBodyHtml = true;
                mail.To.Add(emailTo);
                mail.Subject = "Phản hồi liên hệ";
                mail.Body = "<p style='text-align: left;'>Nội dung khách hàng: "+message+"</p>" +
                            "<p style='text-align:left;'>Nội dung phản hồi: " + repMessage+"</p>"+
                            "<p style='text-align: left;'>Cảm ơn quý khách đã liên hệ và tin dùng sản phẩm của Fruit-Kha.</p>";
                SmtpServer.Port = int.Parse(port.Value);
                SmtpServer.Credentials = new System.Net.NetworkCredential(emailaddress.Value, password.Value);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
