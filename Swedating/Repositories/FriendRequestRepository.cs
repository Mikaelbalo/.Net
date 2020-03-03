using Swedating.Entities;
using Swedating.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Swedating.Repositories
{
	public class FriendRequestRepository
	{

		public List<FriendRequest> GetFriendRequests(string userId)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var friends = _ctx.Set<FriendRequest>().Where(x => x.FriendUserId == userId && !x.IsFriend).Include(x=>x.UserProfile);
				return friends.ToList();
			}
		}
		

		public FriendRequest AcceptFriendRequest(int friendRequestId)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var friends = _ctx.Set<FriendRequest>().Where(x => x.Id == friendRequestId);

				var friendRequest = friends.FirstOrDefault();

				friendRequest.IsFriend = true;

				_ctx.Entry(friendRequest).State = EntityState.Modified;

				_ctx.SaveChanges();

				return friendRequest;
			}
		}


		public FriendRequest GetById(int friendRequestId)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var friends = _ctx.Set<FriendRequest>().Where(x => x.Id == friendRequestId);
				
				return friends.FirstOrDefault();
			}
		}

		public bool Exists(string userId, string friendUserId)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var friends = _ctx.Set<FriendRequest>().Where(x => (x.UserId == userId && x.FriendUserId == friendUserId) || (x.UserId == friendUserId && x.FriendUserId == userId));
				return friends.FirstOrDefault() != null;
			}
		}
		public bool AreFriends(string userId, string friendUserId)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var friends = _ctx.Set<FriendRequest>().Where(x => (x.UserId == userId && x.FriendUserId == friendUserId) || (x.UserId == friendUserId && x.FriendUserId == userId));
				return friends.FirstOrDefault() != null && friends.FirstOrDefault().IsFriend;
			}
		}

		public FriendRequest Add(FriendRequest friend)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var foundFriendRequest = _ctx.FriendRequests.Any(x => x.FriendUserProfileId == friend.FriendUserProfileId && x.UserProfileId == friend.UserProfileId);
				if (!foundFriendRequest)
				{
					_ctx.Entry<FriendRequest>(friend).State = System.Data.Entity.EntityState.Added;
					_ctx.SaveChanges();
				}
				return friend;
			}
		}

		public void DeclineFriendRequest(int friendRequestId)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var friendRequest = GetById(friendRequestId);
				if (friendRequest != null)
				{
					_ctx.Entry<FriendRequest>(friendRequest).State = System.Data.Entity.EntityState.Deleted;
					_ctx.SaveChanges();
				}
				
			}
		}
	}
}