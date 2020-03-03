using Microsoft.AspNet.Identity;
using Swedating.Helpers;
using Swedating.Managers;
using System;
using System.Web.Mvc;

namespace Swedating.Controllers
{
    [Authorize]
    public class UserFriendController : Controller
    {
        private readonly ProfileManager _profileManager;
        private readonly FriendProfileManager _friendProfileManager;

        public UserFriendController()
        {
            _profileManager = new ProfileManager();
            _friendProfileManager = new FriendProfileManager();
        }

        [CustomerAuthorize]
        public ActionResult Index()
        {
            var currentProfile = _profileManager.GetByUserId(User.Identity.GetUserId());
            var friends = _friendProfileManager.GetAll(currentProfile.Id);
            return View(friends);
        }
     
    }
}