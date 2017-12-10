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
    public class ItemController : Controller
    {
        private ItemBusiness _ItemBusiness = new ItemBusiness();
        private BetBusiness _BetBusiness = new BetBusiness();
        public ActionResult MyIndex()
        {
            return View(_ItemBusiness.GetMyTrade(User.Identity.Name));
        }
        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        public ActionResult Create(ItemModelview model)
        {
            model.UserName = User.Identity.Name;
            _ItemBusiness.CreateItem(model);
            return RedirectToAction("MyIndex");
        }
        public ActionResult Index()
        {
            return View(_ItemBusiness.GetAllItemWithImage());
        }
        public ActionResult Details(string id)
        {
            Session["Itid"] = id;

            var item = _ItemBusiness.DetailsItem(id);
            if (item == null)
            {
                return View();
            }
            return View(item);
        }
       
        public ActionResult Edit(string id)
        {
            id = "UVWS2017111123";
            var item = _BetBusiness.GetItemToBet(id);
            return View(item);
        }
        public ActionResult SearchItem(string srearch)
        {
            return View();
        }
        public ActionResult CreateTrade()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Item/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}