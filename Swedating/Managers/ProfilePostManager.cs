using Swedating.Entities;
using Swedating.Repositories;
using System;
using System.Collections.Generic;

namespace Swedating.Managers
{
	public class ProfilePostManager
	{
		private readonly ProfilePostRepository _friendRepository;

		public ProfilePostManager()
		{
			_friendRepository = new ProfilePostRepository();
		}

		//Get all profile posts by userProfile
		public List<ProfilePost> GetAll(int userProfileId)
		{
			try
			{
				return _friendRepository.GetAll(userProfileId);

			}
			catch (Exception e)
			{
				
				throw new Exception("Error -  Get all posts.");
			}
		}
		
		public ProfilePost Add(ProfilePost profilePost)
		{
			try
			{
				return _friendRepository.Add(profilePost);

			}
			catch (Exception e)
			{
				
				throw new Exception("Error - Add a post.");
			}
		}

		
	}
}