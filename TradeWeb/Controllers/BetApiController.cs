using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Trade.BusinessLogic.Business;
using Trade.Model.ModelView;

namespace TradeWeb.Controllers
{
    [Authorize]
    [RoutePrefix("api/Bet")]
    public class BetApiController : ApiController
    {
        private BetBusiness _BetBusiness = new BetBusiness();

        [Route("CreateBet")]
        public IHttpActionResult Bet(BetModelView model)
        {

            model.BetterName = ClaimsPrincipal.Current.Identity.Name;
            _BetBusiness.CreateBetApp(model);
            return Ok();
        }
        [Route("Notify")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public int Get()
        {
            return _BetBusiness.NumberOfBet(ClaimsPrincipal.Current.Identity.Name);
        }
        [Route("MyGetNotification")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]

        public IEnumerable<BetModelView> MyGetNotification()
        {
            return _BetBusiness.MyNotUpdateBet(ClaimsPrincipal.Current.Identity.Name);
        }
        [Route("UpdateBet")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public bool UpdateBet(UpdateBetModelView Updatemodel)
        {
            return _BetBusiness.UpdateBet(Updatemodel);
        }
        [Route("GetUpdatedBet")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]

        public IEnumerable<BetModelView> GetUpdatedBet()
        {
            return _BetBusiness.GetUpdatedBet(ClaimsPrincipal.Current.Identity.Name);
        }
        [Route("UpdateRead/{itemref}")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult UpdateRead(string itemref)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _BetBusiness.UpdateRead(itemref);
            return Ok();
        }

    }
}
