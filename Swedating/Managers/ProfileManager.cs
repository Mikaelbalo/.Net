using Swedating.Entities;
using Swedating.Enums;
using Swedating.Repositories;
using System;
using System.Collections.Generic;

namespace Swedating.Managers
{
	public class ProfileManager
	{
		private readonly ProfileRepository _profileRepository;

		public ProfileManager()
		{
			_profileRepository = new ProfileRepository();
		}

		//Get all profiles
		public List<UserProfile> GetAll()
		{
			try
			{

				return _profileRepository.GetAll();

			}
			catch (Exception e)
			{				
				throw new Exception("Error - Get user's profile.");
			}
		}

		//Get all profiles by filters	
		public List<UserProfile> GetByFilters(string searchString)
		{
			try
			{				
				return _profileRepository.GetByFilters(searchString);

			}
			catch (Exception e)
			{
				throw new Exception("Error - Get user's profile.");
			}
		}


		//Get profile by user id
		public UserProfile GetByUserId(string userId)
		{
			try
			{
				return _profileRepository.GetByUserId(userId);

			}
			catch (Exception e)
			{				
				throw new Exception("Error - Get the user's profile.");
			}
		}

		//Get profile by username
		public UserProfile GetByUserName(string userName)
		{
			try
			{
				return _profileRepository.GetByUserName(userName);

			}
			catch (Exception e)
			{				
				throw new Exception("Error - Get the user's profile.");
			}
		}

		//Get profile by id
		public UserProfile GetById(int userProfileId)
		{
			try
			{
				return _profileRepository.GetById(userProfileId);

			}
			catch (Exception e)
			{
				
				throw new Exception("Error - Get the user's profile.");
			}
		}

		//Get last five profiles.	
		public List<UserProfile> GetFirstFiveProfiles()
		{
			try
			{
				return _profileRepository.GetFirstFiveProfiles();

			}
			catch (Exception e)
			{				
				throw new Exception("Error - Get the users's profile.");
			}
		}
				
		//Add profile
		public UserProfile Add(UserProfile profile)
		{
			try
			{
				return _profileRepository.Add(profile);

			}
			catch (Exception e)
			{				
				throw new Exception("Error - Add the user's profile.");
			}
		}

		//Update profile
		public UserProfile Update(UserProfile profile)
		{
			try
			{
				return _profileRepository.Update(profile);

			}
			catch (Exception e)
			{				
				throw new Exception("Error - Update the user's profile.");
			}
		}
	}
}