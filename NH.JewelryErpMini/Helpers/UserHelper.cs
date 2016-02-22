using NH.JewelryErpMini.Helpers;
using NH.JewelryErpMini.Models.viewDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace NH.JewelryErpMini.Helpers
{
    public class UserHelper
    {
        public readonly static UserHelper Instance = new UserHelper();

        public UserDto GetUser()
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket Ticket = FormsAuthentication.Decrypt(authCookie.Value);
                return SerializeHelper.JsonDeserialize<UserDto>(Ticket.UserData);
            }
            return null;
        }
    }
}