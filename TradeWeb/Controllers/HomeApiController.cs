using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Trade.BusinessLogic.Business;
using Trade.Model.ModelView;

namespace TradeWeb.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeApiController : ApiController
    {
        private ImageStoreBusiness _ImageStoreBusiness = new ImageStoreBusiness();
        private ItemBusiness _ItemBusiness = new ItemBusiness();
        [Route("Index")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public IEnumerable<SliderModelViewApp> Index()
        {
            return _ImageStoreBusiness.GetImageForEachUserApp();
            //  return null;
        }


    }
}
