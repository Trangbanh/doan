using Fruit.Base.Models;
using FruitKha.Areas.Admin.Controllers;
using FruitKha.Areas.Admin.Models;
using FruitKha.Base;
using FruitKha.Base.Extension;
using FruitKha.Base.Session;
using FruitKha.Controllers.Base;
using FruitKha.Models;
using Microsoft.AspNetCore.Mvc;

namespace FruitKha.Controllers
{
	public class LoginController : BaseAuthController<LoginController>
	{
		public LoginController(ILogger<LoginController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
		{
		}
		public IActionResult Index()
		{
			return View();
		}
		public JsonResult Login(string username, string password)
		{
			try
			{
				if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
				{
					return Error("Vui lòng nhập tài khoản và mật khẩu");
				}
				var pashHash = Hash.sha256(password.Trim());
				var data = _base.users.Read(x => x.UseName.Equals(username.Trim()) && x.Password.Equals(pashHash) && x.IsActive == true);
				if (data == null)
				{
					return Error("Thông tin tài khoản hoặc mật khẩu không chính xác");
				}
				SetUser(new CustomerSession
				{
					CustomerId = data.UseId,
					Username = data.UseName,
					FullName = data.Name,
					PhoneNumber = data.Sdt,
					Address = data.Address

				});
				return Success("Đăng nhập thành công");
			}
			catch (Exception ex)
			{
				return Error(ex.Message);
			}
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public JsonResult Register(RegisterModel model)
		{
			try
			{
				if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.ConfirmPassword))
				{
					return Error("Vui lòng nhập đầy đủ thông tin");
				}
				if (!model.Password.Trim().Equals(model.ConfirmPassword.Trim()))
				{
					return Error("Mật khẩu xác nhận không trùng mới mật khẩu mới");
				}
				var ckEmail = _base.users.Read(x => x.UseName.Trim().Equals(model.UserName.Trim()));
				if (ckEmail != null)
				{
					return Error("Thông tin tài khoản đã tồn tại");
				}
				
				var hashPass = Hash.sha256(model.Password);
				var user = new User
				{
					Name = model.Name,
					UseName = model.UserName,
					Password = hashPass,
					IsActive = true,
					Sdt = model.PhoneNumber
				};
				_base.users.Insert(user);
				_base.Commit();
				return Data(new { status = true, message = "Đăng ký tài khoản thành công"});
			}
			catch (Exception ex)
			{
				return Error("Lỗi " + ex);
			}
		}
        public IActionResult Logout()
        {
            ClearUser();
            return Redirect("/Login/Index");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassWord(ChangePasswordUser data)
        {
            try
            {
                var user = GetUser();
                if (user == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                var dataUser = _base.users.GetByID(user.CustomerId);
                if (dataUser == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                if (string.IsNullOrEmpty(data.OldPassword))
                {
                    return Error("Vui lòng nhập mật khẩu cũ");
                }
                if (string.IsNullOrEmpty(data.NewPassword))
                {
                    return Error("Vui lòng nhập mật khẩu mới");
                }
                if (string.IsNullOrEmpty(data.ConfirmPassword))
                {
                    return Error("Vui lòng nhập mật khẩu xác nhận");
                }
                var hashPassOld = Hash.sha256(data.OldPassword);
                if (!dataUser.Password.Equals(hashPassOld))
                {
                    return Error("Mật khẩu cũ không chính xác");
                }
                if (data.NewPassword.Equals(data.OldPassword))
                {
                    return Error("Mật khẩu mới không thể trùng với mật khẩu cũ");
                }
                if (!data.ConfirmPassword.Equals(data.NewPassword))
                {
                    return Error("Mật khẩu mới và mật khẩu xác nhận không trùng nhau");
                }
                var hashPassNew = Hash.sha256(data.NewPassword);
                dataUser.Password = hashPassNew;
                _base.users.Update(dataUser);
                _base.Commit();
                return Success("Đổi mật khẩu thành công");
            }
            catch (Exception ex)
            {
                return Error("Lỗi " + ex);
            }
        }
        [HttpGet]
        public IActionResult Profile()
        {
            User model = _base.users.GetByID(GetUser().CustomerId);
            return View(model);
        }
        [HttpPost]
        public JsonResult Profile(User data)
        {
            try
            {
                var entity = _base.users.GetByID(GetUser().CustomerId);
                if (entity == null)
                {
                    return Error("Không tìm thấy thông tin tài khoản của bạn");
                }
                entity.Name = data.Name;
                entity.Sdt = data.Sdt;
                entity.Address = data.Address;
                _base.users.Update(entity);
                _base.Commit();
                return Success("Thay đổi thông tin thành công");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
		[HttpGet]
		public IActionResult ResetPassword()
		{
			return View();
		}
		[HttpPost]
		public JsonResult ResetPassword(string phone, string newPassword, string confirmPassword)
		{
			try
			{
				if(string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
				{
					return Error("Vui lòng nhập đầy đủ thông tin");
				}
				var entity = _base.users.Read(x => x.Sdt.Trim().Equals(phone.Trim()));
				if (entity == null)
				{
					return Error("Số điện thoại không tồn tại trong hệ thống");
				}
				if (!newPassword.Trim().Equals(confirmPassword.Trim()))
				{
					return Error("Mật khẩu xác nhận không trùng với mật khẩu mới");
				}
				var hashPass = Hash.sha256(newPassword);
				entity.Password = hashPass;
				_base.users.Update(entity);
				_base.Commit();
				return Success("Đặt lại mật khẩu thành công");
			}
			catch (Exception ex)
			{
				return Error(ex.Message);
			}
		}
	}
}
