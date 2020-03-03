using Swedating.Entities;
using Swedating.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Swedating.Repositories
{
	public class ProfilePostRepository
	{
		public List<ProfilePost> GetAll(int userProfileId)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var friends = _ctx.Set<ProfilePost>().Where(x => x.UserProfileId == userProfileId).OrderByDescending(x=>x.Date).Include(x => x.UserProfile).Include(x => x.FriendUserProfile);
				return friends.ToList();
			}
		}

		public ProfilePost Add(ProfilePost profilePost)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				_ctx.Entry(profilePost).State = EntityState.Added;
				_ctx.SaveChanges();

				return profilePost;
			}
		}
	}
}