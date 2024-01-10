using FruitKha.Base;
using FruitKha.Base.Session;
using FruitKha.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace FruitKha.Controllers
{
    public class CartController : BaseAuthController<CartController>
    {
        public CartController(ILogger<CartController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IBase @base) : base(@base, httpContextAccessor, configuration, logger)
        {
        }
        public IActionResult Index()
		{
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
        public JsonResult AddToCart(int productId, int quantity)
        {
            try
            {
                var cart = new Cart
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                var lstCart = new List<Cart>();
                var getCart = CartCookie.GetCartCoookie(_contextAccessor);
                if (getCart == null)
                {
                    lstCart.Add(cart);
                    bool flag = CartCookie.SetCartCookie(_contextAccessor, lstCart);
                    if (!flag)
                    {
                        return Error("Thêm giỏ hàng không thành công");
                    }
                }
                else
                {
                    var ckProductId = getCart.Where(x => x.ProductId == productId).FirstOrDefault();
                    if (ckProductId != null)
                    {
                        var carts = new Cart
                        {
                            ProductId = productId,
                            Quantity = quantity + ckProductId.Quantity
                        };
                        lstCart = getCart;
                        lstCart.RemoveAll(x => x.ProductId == productId);
                        lstCart.Add(carts);
                        bool flags = CartCookie.SetCartCookie(_contextAccessor, lstCart);
                        if (!flags)
                        {
                            return Error("Thêm giỏ hàng không thành công");
                        }
                    }
                    else
                    {
                        lstCart = getCart;
                        lstCart.Add(cart);
                        bool flags = CartCookie.SetCartCookie(_contextAccessor, lstCart);
                        if (!flags)
                        {
                            return Error("Thêm giỏ hàng không thành công");
                        }
                    }
                }
                int totalCart = lstCart.Count();
                return Data(new { status = true, message = "Thêm giỏ hàng thành công", total = totalCart });

            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
        public JsonResult UpdateCart(int productId, int quantity)
        {
            var cart = new Cart
            {
                ProductId = productId,
                Quantity = quantity
            };
            var lstCart = new List<Cart>();
            var getCart = CartCookie.GetCartCoookie(_contextAccessor);
            if (getCart != null)
            {
                var ckProductId = getCart.Where(x => x.ProductId == productId).FirstOrDefault();
                if (ckProductId != null)
                {
                    lstCart = getCart;
                    lstCart.RemoveAll(x => x.ProductId == productId);
                    lstCart.Add(cart);
                    bool flags = CartCookie.SetCartCookie(_contextAccessor, lstCart);
                    if (!flags)
                    {
                        return Error("Không cập nhật được số lượng sản phẩm");
                    }
                }
                else
                {
                    return Error("Không tìm thấy thông tin sản phẩm");
                }
            }
            else
            {
                return Error("Hết phiên làm việc của giỏ hàng. Vui lòng chọn lại sản phẩm bạn cần mua ");
            }
            return Success("");
        }
        public JsonResult DeleteCart(int productId)
        {
            var lstCart = new List<Cart>();
            var getCart = CartCookie.GetCartCoookie(_contextAccessor);
            if (getCart != null)
            {
                var ckProductId = getCart.Where(x => x.ProductId == productId).FirstOrDefault();
                if (ckProductId != null)
                {
                    lstCart = getCart;
                    lstCart.RemoveAll(x => x.ProductId == productId);
                    bool flags = CartCookie.SetCartCookie(_contextAccessor, lstCart);
                    if (!flags)
                    {
                        return Error("Không xóa được sản phẩm");
                    }
                }
                else
                {
                    return Error("Không tìm thấy thông tin sản phẩm");
                }
            }
            else
            {
                return Error("Thông tin giỏ hàng không tồn tại");
            }
            return Success("");
        }
    }
}
