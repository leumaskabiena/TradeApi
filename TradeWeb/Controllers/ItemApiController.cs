using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using Trade.BusinessLogic.Business;
using Trade.Model.ModelView;

namespace TradeWeb.Controllers
{
    [RoutePrefix("api/item")]
    [Authorize]
    public class ItemApiController : ApiController
    {

        private ItemBusiness _ItemBusiness = new ItemBusiness();
        [Route("Create")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Create(ItemModelview model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            model.UserName = ClaimsPrincipal.Current.Identity.Name; ;
            _ItemBusiness.CreateItem(model);
            return Ok();
        }

        [Route("MyIndex")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public IEnumerable<ItemModelview> MyIndex()
        {
            return _ItemBusiness.GetMyTrade(ClaimsPrincipal.Current.Identity.Name);
        }
        [AllowAnonymous]
        [Route("Details/{id}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public ItemModelview Details(string id)
        {
            return _ItemBusiness.DetailsItemApp(id);
        }

        [Route("Delete/{id}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public bool Delete(string id)
        {
            return _ItemBusiness.DeleteItem(id);
        }
        [Route("Edit")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult Edit(ItemModelview model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _ItemBusiness.PostEditItem(model);
            return Ok();
        }
    }
}