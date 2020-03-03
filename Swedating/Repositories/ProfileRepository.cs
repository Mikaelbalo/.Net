using Swedating.Entities;
using Swedating.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Swedating.Repositories
{
	public class ProfileRepository
	{
		public List<UserProfile> GetFirstFiveProfiles()
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var profile = _ctx.Set<UserProfile>().OrderBy(x=>x.CreatedDate).Take(5);
				return profile.ToList();
			}
		}

		public UserProfile GetByUserName(string userName)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var profile = _ctx.Set<UserProfile>().Where(x => x.UserName == userName);
				return profile.FirstOrDefault();

			}
		}

		public List<UserProfile> GetAll()
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var profiles = _ctx.Set<UserProfile>();
				return profiles.ToList();
			}
		}

		public List<UserProfile> GetByFilters(string searchString)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var profile = _ctx.Set<UserProfile>().Where(x => ((string.IsNullOrEmpty(searchString) || x.UserName.Contains(searchString)) 
														|| (x.FirstName.Contains(searchString)  || x.LastName.Contains(searchString))));
				return profile.ToList();
			}
		}

		public UserProfile GetByUserId(string userId)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var profile = _ctx.Set<UserProfile>().Where(x => x.UserId == userId);
				return profile.FirstOrDefault();

			}
		}

		public UserProfile GetById(int userProfileId)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var profile = _ctx.Set<UserProfile>().Where(x => x.Id == userProfileId);
				return profile.FirstOrDefault();
			}
		}

		public UserProfile Add(UserProfile userProfile)
		{
			using (var _ctx = new ApplicationDbContext())
			{				
				_ctx.Entry<UserProfile>(userProfile).State = System.Data.Entity.EntityState.Added;
				_ctx.SaveChanges();
				return userProfile;
			}
		}

		public UserProfile Update(UserProfile userProfile)
		{
			using (var _ctx = new ApplicationDbContext())
			{
				var oldProfile = GetByUserId(userProfile.UserId);
				_ctx.Entry(oldProfile).State = EntityState.Detached;
				_ctx.Entry(userProfile).State = EntityState.Modified;
				_ctx.SaveChanges();
				return userProfile;
			}
		}

	}
}