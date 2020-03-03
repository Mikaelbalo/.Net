using Microsoft.AspNet.Identity;
using Swedating.Managers;
using System.Web.Mvc;

namespace Swedating.Helpers
{
	//Allows redirecting to the profile registration view if the user does not have a profile created.
	public class CustomerAuthorize : AuthorizeAttribute
	{
		private readonly ProfileManager _profileManager = new ProfileManager();
		
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			var profile = _profileManager.GetByUserId(filterContext.HttpContext.User.Identity.GetUserId());

			// If they are authorized, handle accordingly
			if (this.AuthorizeCore(filterContext.HttpContext) && profile != null)
			{
				base.OnAuthorization(filterContext);
			}
			else
			{
				// Otherwise redirect to your specific authorized area
				filterContext.Result = new RedirectResult("~/UserProfile/Create");
			}
		}
	}
}