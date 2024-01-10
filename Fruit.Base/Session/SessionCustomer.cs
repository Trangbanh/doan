using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace FruitKha.Base.Session
{
    public static class SessionCustomer
    {
        public static CustomerSession GetUser(IHttpContextAccessor _httpContextAccessor)
        {
            var userSession = _httpContextAccessor.HttpContext.Session.GetString("customer");
            if (userSession != null)
            {
                return JsonConvert.DeserializeObject<CustomerSession>(userSession);
            }
            return null;
        }
    }
}
