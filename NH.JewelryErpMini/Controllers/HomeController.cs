using NH.JewelryErpMini.Models.Initial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NH.JewelryErpMini.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //private JewelryDbContext db;
        //public HomeController()
        //{
        //    db = new JewelryDbContext();
        //}
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}