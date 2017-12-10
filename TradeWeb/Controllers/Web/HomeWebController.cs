using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trade.BusinessLogic.Business;

namespace TradeWeb.Controllers.Web
{
    public class HomeWebController : Controller
    {
        private ImageStoreBusiness _ImageStoreBusiness = new ImageStoreBusiness();
        // GET: HomeWeb
        public ActionResult Index()
        {
            return View(_ImageStoreBusiness.GetImageForEachUser());
        }

        public ActionResult TradeIndex()
        {
            return View(_ImageStoreBusiness.GetAllImageForEachUser());
        }
        public ActionResult GetNotification()
        {
            ViewBag.notication = "kab";
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
    }
}