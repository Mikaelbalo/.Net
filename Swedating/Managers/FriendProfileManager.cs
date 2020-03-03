using Swedating.Entities;
using Swedating.Repositories;
using System;
using System.Collections.Generic;

namespace Swedating.Managers
{
	public class FriendProfileManager
	{
		private readonly FriendProfileRepository _friendRepository;

		public FriendProfileManager()
		{
			_friendRepository = new FriendProfileRepository();
		}

		//Get a friendProfile by profile id
		public FriendRelationshiop GetById(int friendUserProfileId)
		{
			try
			{
				return _friendRepository.GetById(friendUserProfileId);

			}
			catch (Exception e)
			{				
				throw new Exception("Error - Get a friend profile.");
			}
		}

		//Get all friendProfiles by profile id
		public List<FriendRelationshiop> GetAll(int userProfileId)
		{
			try
			{
				return _friendRepository.GetAll(userProfileId);

			}
			catch (Exception e)
			{
			
				throw new Exception("Error - Get all friends.");
			}
		}

	
		//Add friendProfile
		public FriendRelationshiop Add(FriendRelationshiop friendProfile)
		{
			try
			{
				return _friendRepository.Add(friendProfile);

			}
			catch (Exception e)
			{
				throw new Exception("Error - add the user's friend.");
			}
		}

		//Update friendProfile
		public FriendRelationshiop Update(FriendRelationshiop friendProfile)
		{
			try
			{
				return _friendRepository.Update(friendProfile);

			}
			catch (Exception e)
			{
				throw new Exception("Error - update the user's friend.");
			}
		}
	}
}