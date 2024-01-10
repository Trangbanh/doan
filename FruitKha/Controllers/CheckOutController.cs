using Fruit.Base.Models;
using FruitKha.Base;
using FruitKha.Base.Session;
using FruitKha.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace FruitKha.Controllers
{
    public class CheckOutController : BaseAuthController<CheckOutController>
    {
        public CheckOutController(ILogger<CheckOutController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {
        }
        public IActionResult Index()
        {
			if(GetUser() == null)
			{
				return RedirectToAction("Index", "Login");
			}
			decimal totalPrice = 0;
			List<CartObject> model = new List<CartObject>();
			var cookie = CartCookie.GetCartCoookie(_contextAccessor);
			var cartObj = CartCookie.GetCartObjects(cookie, _base);
			if (cartObj != null && cartObj.Count > 0)
			{
				model = cartObj.OrderByDescending(x => x.ProductId).ToList();
				totalPrice = model.Sum(x => x.TotalPrice);
			}
			ViewBag.TotalPriceAll = totalPrice;
			return View(model);
        }
		public JsonResult PaymentCheckOut(string phone, string address)
		{
			try
			{
				var user = GetUser();
				if (user == null)
				{
					return Error("Vui lòng đăng nhập để thanh toán ");
				}
				if (string.IsNullOrEmpty(phone))
				{
					return Error("Vui lòng nhập số điện thoại");
				}
				if (string.IsNullOrEmpty(address))
				{
					return Error("Vui lòng nhập địa chỉ giao hàng");
				}
				var cookie = CartCookie.GetCartCoookie(_contextAccessor);
				if (cookie == null)
				{
					return Error("Hết phiên lưu thông tin sản phẩm trong giỏ hàng");
				}
				var cartObj = CartCookie.GetCartObjects(cookie, _base);
				if (cartObj != null && cartObj.Count > 0)
				{
					var orderCode = DateTime.Now.ToString("yyMMddhhmmssfff");
					var orderModel = new Bill
					{
						OrderCode = orderCode,
						UseId = user.CustomerId,
						Status = false,
						Phone = phone,
						Address = address,
						OrderDate = DateTime.Now
					};
					int orderId = _base.bills.InserOrder(orderModel);
					if (orderId == 0)
					{
						return Error("Có lỗi xảy ra, không thể đặt hàng được");
					}
					foreach (var item1 in cartObj)
					{
						var orderDetail = new DetailedInvoice
						{
							Idbill = orderId,
							ProductId = item1.ProductId,
							TotalPrice = item1.Price * item1.Quantity,
							Quantity = item1.Quantity,
						};
						_base.detailedInvoice.Insert(orderDetail);
					}
					_base.Commit();
					CartCookie.ClearCartCookie(_contextAccessor);
					return Success("Đặt hàng thành công");
				}
				else
				{
					return Error("Hết phiên lưu thông tin sản phẩm trong giỏ hàng");
				}
			}
			catch (Exception ex)
			{
				return Error(ex.Message);
			}
		}
	}
}
