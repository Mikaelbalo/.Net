using Swedating.Models;
using FluentValidation.Results;
using Microsoft.AspNet.Identity;
using Swedating.Entities;
using Swedating.Enums;
using Swedating.Helpers;
using Swedating.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Swedating.Validators;

namespace Swedating.Controllers
{
	[Authorize]
	public class UserProfileController : Controller
	{
		private readonly ProfileManager _profileManager;
		private readonly FriendRequestManager _friendRequestManager;
		private readonly FriendProfileManager _friendProfileManager;

		private readonly ProfilePostManager _profilePostManager;

		public UserProfileController()
		{
			_profileManager = new ProfileManager();
			_friendRequestManager = new FriendRequestManager();
			_friendProfileManager = new FriendProfileManager();

			_profilePostManager = new ProfilePostManager();
		}

		//View all profiles.  Search by username or first name or last name. 
		//Only profiles that match the user's sexual orientation.
		[CustomerAuthorize]
		public ActionResult Profiles(string searchString)
		{
			try
			{
				var profile = _profileManager.GetByUserId(User.Identity.GetUserId());

				var profiles = _profileManager.GetByFilters(searchString);

				return View(profiles.ToList());
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "ErrorHandler", new { @message = e.Message });
			}
		}


		//View my profile
		[CustomerAuthorize]
		public ActionResult ViewProfile(int userProfileId)
		{
			try
			{

				var currentProfile = _profileManager.GetByUserId(User.Identity.GetUserId());
				if (userProfileId == currentProfile.Id)
					return RedirectToAction("MyProfile");

				var userProfileViewModel = new UserProfileViewModel();

				userProfileViewModel.UserProfile = _profileManager.GetById(userProfileId); 
				userProfileViewModel.Friends = _friendProfileManager.GetAll(userProfileViewModel.UserProfile.Id); 

				ViewBag.UserProfileId = userProfileId;
				ViewBag.FriendUserProfileId = currentProfile.Id;
				ViewBag.IsFriend = _friendRequestManager.AreFriends(User.Identity.GetUserId(), userProfileViewModel.UserProfile.UserId); 
				ViewBag.PendingFriend = _friendRequestManager.Exists(User.Identity.GetUserId(), userProfileViewModel.UserProfile.UserId);

				userProfileViewModel.Friends = _friendProfileManager.GetAll(userProfileViewModel.UserProfile.Id); 
				userProfileViewModel.ProfilePosts = _profilePostManager.GetAll(userProfileViewModel.UserProfile.Id); 


				return View("Profile", userProfileViewModel);
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "ErrorHandler", new { @message = e.Message });
			}
		}

		// MyProfile
		[CustomerAuthorize]
		public ActionResult MyProfile()
		{
			try
			{
				var userProfileViewModel = new UserProfileViewModel();
				var currentProfile = _profileManager.GetByUserId(User.Identity.GetUserId());
				ViewBag.UserProfileId = currentProfile.Id;
				ViewBag.FriendUserProfileId = currentProfile.Id;
				userProfileViewModel.UserProfile = _profileManager.GetByUserId(User.Identity.GetUserId());
				userProfileViewModel.ProfilePosts = _profilePostManager.GetAll(userProfileViewModel.UserProfile.Id);
				userProfileViewModel.Friends = _friendProfileManager.GetAll(userProfileViewModel.UserProfile.Id);
				return View("Profile", userProfileViewModel);
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "ErrorHandler", new { @message = e.Message });
			}
		}


		public ActionResult Create()
		{
			try
			{
				var profile = new UserProfile();
				var genders = from GenderEnum g in Enum.GetValues(typeof(GenderEnum))
							  select new EnumValues { ID = (int)g, Name = g.ToString() };
				ViewBag.Genders = new SelectList(genders, "ID", "Name");

				return View("Create", profile);
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "ErrorHandler", new { @message = e.Message });
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(UserProfile model)
		{
			var userId = User.Identity.GetUserId();

			var newUserProfile = new UserProfile
			{
				UserId = userId,
				UserName = model.UserName,
				FirstName = model.FirstName,
				LastName = model.LastName,
				BirthDate = model.BirthDate,
				CreatedDate = DateTime.Today,
				Email = User.Identity.GetUserName(),
				GenderId = model.GenderId,


			};

			var genders = from GenderEnum g in Enum.GetValues(typeof(GenderEnum))
						  select new EnumValues { ID = (int)g, Name = g.ToString() };
			ViewBag.Genders = new SelectList(genders, "ID", "Name");

			ProfileValidator validator = new ProfileValidator();

			ValidationResult validationResults = validator.Validate(model);


			// Add the errors from our validation into the error messages variable and return it.
			if (!validationResults.IsValid)
			{
				foreach (var error in validationResults.Errors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

				}
			}

			if (ModelState.IsValid)
			{
				_profileManager.Add(newUserProfile);

				return RedirectToAction("MyProfile", "UserProfile");
			}

			return View("Create", model);

		}

		[CustomerAuthorize]
		public ActionResult Edit()
		{
			try
			{
				var profile = _profileManager.GetByUserId(User.Identity.GetUserId());

				var genders = from GenderEnum g in Enum.GetValues(typeof(GenderEnum))
							  select new EnumValues { ID = (int)g, Name = g.ToString() };
				ViewBag.Genders = new SelectList(genders, "ID", "Name");

				return View("Edit", profile);
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "ErrorHandler", new { @message = e.Message });
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(UserProfile model)
		{
			var userId = User.Identity.GetUserId();
			var currentUserProfile = _profileManager.GetByUserId(userId);


			currentUserProfile.UserName = model.UserName;
			currentUserProfile.FirstName = model.FirstName;
			currentUserProfile.LastName = model.LastName;
			currentUserProfile.BirthDate = model.BirthDate;
			currentUserProfile.Email = User.Identity.GetUserName();
			currentUserProfile.GenderId = model.GenderId;


			if (ModelState.IsValid)
			{
				_profileManager.Update(currentUserProfile);

				return RedirectToAction("MyProfile", "UserProfile");
			}

			var genders = from GenderEnum g in Enum.GetValues(typeof(GenderEnum))
						  select new EnumValues { ID = (int)g, Name = g.ToString() };
			ViewBag.Genders = new SelectList(genders, "ID", "Name");


			return View("Edit", model);
		}




		[Authorize]
		[HttpPost]
		public ActionResult UpdateProfilePicture(HttpPostedFileBase pictureFile)
		{
			try
			{
				string userId = User.Identity.GetUserId();

				var currentUserProfile = _profileManager.GetByUserId(userId);

				//If the user already has a profile picture, we delete it.
				if (currentUserProfile.PicturePath != null)
				{
					string fullPath = Request.MapPath("~" + currentUserProfile.PicturePath);
					if (System.IO.File.Exists(fullPath))
					{
						System.IO.File.Delete(fullPath);
						currentUserProfile.PicturePath = null;
						_profileManager.Update(currentUserProfile);
					}
				}

				string fileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);
				string extension = Path.GetExtension(pictureFile.FileName);
				fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
				fileName = fileName.Replace(" ", "_");
				currentUserProfile.PicturePath = "~/Images/" + fileName;
				string PicturePath = "/Images/" + fileName;

				//Save image into images directory
				fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
				pictureFile.SaveAs(fileName);

				currentUserProfile.PicturePath = PicturePath;

				//Update profile picture
				_profileManager.Update(currentUserProfile);


			}
			catch (Exception e)
			{
				return RedirectToAction("Index", "ErrorHandler", new { @message = e.Message });
			}
			return RedirectToAction("MyProfile", "UserProfile");

		}

		//View all pending friend request
		[CustomerAuthorize]
		public ActionResult FriendRequests()
		{

			var userId = User.Identity.GetUserId();
			var friendsList = _friendRequestManager.GetFriendRequests(userId);
			return View("FriendRequests", friendsList);
		}

		//Count pending friend requests
		public ActionResult CountFriendRequests()
		{

			var userId = User.Identity.GetUserId();
			var currentUserProfile = _profileManager.GetByUserId(userId);
			if (currentUserProfile == null)
			{
				return Content("0");
			}
			var friendsList = _friendRequestManager.GetFriendRequests(userId);

			return Content(friendsList.Count.ToString());
		}



	}
}