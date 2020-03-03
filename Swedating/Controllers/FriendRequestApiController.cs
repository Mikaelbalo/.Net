using Microsoft.AspNet.Identity;
using Swedating.Entities;
using Swedating.Managers;
using System.Web.Http;

namespace Swedating.Controllers
{
	[RoutePrefix("api/FriendRequests")]
	public class FriendRequestApiController : ApiController
	{

		//Add a friend request between friends.
		[Route("FriendRequest/add")]
		[HttpGet]
		public void AddFriend(string userId)
		{
			var friendRequestManager = new FriendRequestManager();
			var profileManager = new ProfileManager();
			var currentUserId = User.Identity.GetUserId();
			var currentProfile = profileManager.GetByUserId(currentUserId);

			var friendProfile = profileManager.GetByUserId(userId);
			var areFriend = friendRequestManager.AreFriends(currentUserId, userId);

			if (!areFriend)
			{
				var friend = new FriendRequest
				{
					UserId = currentUserId,
					UserProfileId = currentProfile.Id,
					FriendUserId = userId,
					FriendUserProfileId = friendProfile.Id,				
					IsFriend = false,

				};

				friendRequestManager.Add(friend);
				
			}
		}

		//Accept a friend request 
		[Route("FriendRequest/accept")]
		[HttpGet]
		public void AcceptFriend(int friendRequestId)
		{
			var friendRequestManager = new FriendRequestManager();
			var friendProfileManager = new FriendProfileManager();
			var friendRequest = friendRequestManager.AcceptFriendRequest(friendRequestId);

			friendProfileManager.Add(new FriendRelationshiop()
			{
				UserProfileId = friendRequest.UserProfileId,
				FriendUserProfileId = friendRequest.FriendUserProfileId
			});

			friendProfileManager.Add(new FriendRelationshiop()
			{
				UserProfileId = friendRequest.FriendUserProfileId,
				FriendUserProfileId = friendRequest.UserProfileId,				

			});
		}

		//Decline a friend request 
		[Route("FriendRequest/decline")]
		[HttpGet]
		public void DeclineFriend(int friendRequestId)
		{
			var friendRequestManager = new FriendRequestManager();

			friendRequestManager.DeclineFriendRequest(friendRequestId);


		}
	}
}
