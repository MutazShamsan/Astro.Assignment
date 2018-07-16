using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Astro.Assignment.Web.FavoriteChannelManagement;
using Astro.Assignment.Web.Models;

namespace Astro.Assignment.Web.Controllers
{
    public class AstroAssignmentOneController : AstroAssignmentBaseController
    {

        public AstroAssignmentOneController()
        {
            FavoriteRepoManagement = new FavoriteRepositoryCache();
        }

        public ActionResult Index(string sortByName, string sortingOrder)
        {
            // var result = await GetChannelsMiniInfo(sortByName, sortingOrder);
            return View();
        }

        public async Task<ActionResult> GetChannelsList()
        {
            var result = await GetChannelsMiniInfo(Request.QueryString["order[0][column]"],
                Request.QueryString["order[0][dir]"], Request.QueryString["start"], Request.QueryString["length"]);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
