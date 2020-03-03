using Swedating.Entities;
using Swedating.Repositories;
using System;
using System.Collections.Generic;

namespace Swedating.Managers
{
	public class FriendRequestManager
	{
		private readonly FriendRequestRepository _friendRequestRepository;

		public FriendRequestManager()
		{
			_friendRequestRepository = new FriendRequestRepository();
		}

		//Get all pending friendRequests by user id
		public List<FriendRequest> GetFriendRequests(string userId)
		{
			try
			{
				return _friendRequestRepository.GetFriendRequests(userId);

			}
			catch (Exception e)
			{				
				throw new Exception("An error occurred while trying to retrieve all friend requests.");
			}
		}


		//Returns true if two users are friends
		public bool AreFriends(string currentUserId, string friendUserId)
		{
			try
			{
				return _friendRequestRepository.AreFriends(currentUserId, friendUserId);

			}
			catch (Exception e)
			{
				
				throw new Exception("An error occurred while trying to retrieve the friendship relationship.");
			}
		}

		//Returns true if exists a friendRequest between friends
		public bool Exists(string currentUserId, string friendUserId)
		{
			try
			{
				return _friendRequestRepository.Exists(currentUserId, friendUserId);

			}
			catch (Exception e)
			{				
				throw new Exception("An error occurred while trying to retrieve the friendship relationship.");
			}
		}		

		//Allows add a friend's friend request
		public FriendRequest Add(FriendRequest friend)
		{
			try
			{
				return _friendRequestRepository.Add(friend);

			}
			catch (Exception e)
			{				
				throw new Exception("Error - Add the user's profile.");
			}
		}

		//Allows accept a friend's friend request
		public FriendRequest AcceptFriendRequest(int friendRequestId)
		{
			try
			{
				return _friendRequestRepository.AcceptFriendRequest(friendRequestId);

			}
			catch (Exception e)
			{				
				throw new Exception("Error - Get friend request by id.");
			}
		}

		//Allows decline a friend's friend request
		public void DeclineFriendRequest(int friendRequestId)
		{
			try
			{
				_friendRequestRepository.DeclineFriendRequest(friendRequestId);

			}
			catch (Exception e)
			{			
				throw new Exception("Error - Remove the user's profile.");
			}
		}
	}
}