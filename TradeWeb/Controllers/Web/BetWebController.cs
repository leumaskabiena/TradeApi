using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trade.BusinessLogic.Business;
using Trade.Model.ModelView;

namespace TradeWeb.Controllers.Web
{
    [Authorize]
    public class BetWebController : Controller
    {
        private BetBusiness _BetBusiness = new BetBusiness();
        [HttpGet]
        public ActionResult Bet(string id)
        {
            id = Session["Itid"].ToString();
            var item = _BetBusiness.GetItemToBet(id);
            return View(item);
        }
        [HttpPost]
        public ActionResult Bet(BetModelView model)
        {
            //model.BetterName = User.Identity.Name;
            model.BetterName = "samuel@gmail.com";
            _BetBusiness.CreateBet(model);
            return RedirectToAction("Details", new { id = model.itemref });
        }
        public ActionResult MyNotUpdateBet()
        {
            return View(_BetBusiness.MyNotUpdateBet(User.Identity.Name));
        }
        public ActionResult Details(string id)
        {
            return View(_BetBusiness.GetBet(id));
        }
        public ActionResult Accept(string id)
        {
            var item = new UpdateBetModelView
            {
                id = id,
                ans = 2
            };
            if (_BetBusiness.UpdateBet(item))
            {
                return RedirectToAction("MyNotUpdateBet");
            }
            else
            {
                return View();
            }

        }
        public ActionResult Decline(string id)
        {
            var item = new UpdateBetModelView
            {
                id = id,
                ans = 1
            };
            if (_BetBusiness.UpdateBet(item))
            {
                return RedirectToAction("MyNotUpdateBet");
            }
            else
            {
                return View();
            }
        }
        public ActionResult GetUpdatedBet()
        {
            return View(_BetBusiness.GetUpdatedBet(User.Identity.Name));
        }
        public ActionResult Viewed(string id)
        {
            _BetBusiness.UpdateRead(id);
            return RedirectToAction("GetUpdatedBet");
        }


    }
}