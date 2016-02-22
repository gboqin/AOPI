using NH.JewelryErpMini.Helpers;
using NH.JewelryErpMini.Models.Repository;
using NH.JewelryErpMini.Models.viewDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NH.JewelryErpMini.Controllers
{
    public class AccountController : Controller
    {
        private AccountRepository repository;
        public AccountController()
        {
            this.repository = new AccountRepository();
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include ="code,password")] LoginDto model)
        {
            Message<UserDto> _message = new Message<UserDto>();
            if (model != null)
            {
                _message = repository.Login(model.code, model.password);
                if (_message.isSuccess)
                {
                    string UserData = SerializeHelper.JsonSerialize<UserDto>(_message.model);

                    FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1, _message.model.usr_Name, DateTime.Now, DateTime.Now.AddHours(4), false, UserData);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(Ticket));
                    Response.Cookies.Add(cookie);
                }
                return Json(_message);
            }
            _message.message = "参数错误，请重新填写！";
            return Json(_message);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult LogError()
        {
            return View();
        }
    }
}