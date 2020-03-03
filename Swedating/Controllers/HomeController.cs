using Swedating.Managers;
using Swedating.Models;
using System.Collections.Generic;
using System.Web.Mvc;


namespace Swedating.Controllers
{
	public class HomeController : Controller
	{
		private readonly ProfileManager _profileManager;
		public HomeController()
		{
			_profileManager = new ProfileManager();
		}

		//Get last five profiles. Recents login.
		public ActionResult Index()
		{
			var profiles = _profileManager.GetFirstFiveProfiles();
			var users = new List<UserViewModel>();
			foreach (var profile in profiles)
			{
				users.Add(new UserViewModel()
				{
					ProfileId = profile.Id,
					UserName = profile.UserName,
					Email = profile.Email,
					PicturePath = profile.PicturePath
				});
			}
			return View(users);
		}

		
	}
}