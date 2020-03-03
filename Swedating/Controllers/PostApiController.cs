using Swedating.Entities;
using Swedating.Managers;
using System;
using System.Web.Http;

namespace Swedating.Controllers
{

	[RoutePrefix("api/Posts")]
	public class PostApiController : ApiController
	{

		private readonly ProfilePostManager _profilePostManager;

		public PostApiController()
		{
		
			_profilePostManager = new ProfilePostManager();
		}

		[Route("post/add")]
		[HttpGet]
		public void Add(int userProfileId, int friendUserProfileId, string message)
		{

			_profilePostManager.Add(new ProfilePost
			{
				Date = DateTime.Now,
				UserProfileId = userProfileId,
				FriendUserProfileId = friendUserProfileId,
				Message = message
			});

		}
		
	}


	


}
