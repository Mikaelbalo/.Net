using Swedating.Entities;
using System.Collections.Generic;

namespace Swedating.Models
{
	public class UserProfileViewModel
	{
		public UserProfile UserProfile { get; set; }
		public List<FriendRelationshiop> Friends { get; set; }
		public List<ProfilePost> ProfilePosts { get; set; }

		public UserProfileViewModel()
		{
			UserProfile = new UserProfile();
			Friends = new List<FriendRelationshiop>();
			ProfilePosts = new List<ProfilePost>();
		}

	}
}