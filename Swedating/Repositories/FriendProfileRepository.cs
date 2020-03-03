using Swedating.Entities;
using Swedating.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Swedating.Repositories
{
	public class FriendProfileRepository
	{
		public FriendRelationshiop GetById(int friendUserProfileId)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var profile = _ctx.Set<FriendRelationshiop>().Where(x => x.Id == friendUserProfileId);
				return profile.FirstOrDefault();

			}
		}

		public List<FriendRelationshiop> GetAll(int userProfileId)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var friends = _ctx.Set<FriendRelationshiop>().Where(x => x.UserProfileId == userProfileId).Include(x=>x.FriendUserProfile).Include(x=>x.UserProfile);
				return friends.ToList();
			}
		}


		public FriendRelationshiop Add(FriendRelationshiop friendRelationship)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var foundFriend = _ctx.Set<FriendRelationshiop>().Any(x => x.FriendUserProfileId == friendRelationship.FriendUserProfileId && x.UserProfileId == friendRelationship.UserProfileId);
				if (!foundFriend)
				{
					_ctx.Entry(friendRelationship).State = EntityState.Added;
					_ctx.SaveChanges();
				}
				return friendRelationship;
			}
		}

		public FriendRelationshiop Update(FriendRelationshiop friendRelationship)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				_ctx.Entry(friendRelationship).State = EntityState.Modified;
				_ctx.SaveChanges();
				return friendRelationship;
			}
		}

	}
}