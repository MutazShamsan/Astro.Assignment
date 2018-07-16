using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Astro.Assignment.Web.FavoriteChannelManagement;
using Astro.Assignment.Web.Models;

namespace Astro.Assignment.Web.Controllers
{
    public class AstroAssignmentThreeController : AstroAssignmentTwoController
    {
        private readonly ApplicationDbContext _dbContext;

        public AstroAssignmentThreeController()
        {
            _dbContext = new ApplicationDbContext();
            FavoriteRepoManagement = new FavoriteRepositoryDatabase(_dbContext);
        }

        // GET: AstroAssignmentThree
        public override ActionResult Index()
        {
            return View();
        }

        protected override string GetFavoriteManagementKey()
        {
            return User.Identity.Name;
        }
    }
}
