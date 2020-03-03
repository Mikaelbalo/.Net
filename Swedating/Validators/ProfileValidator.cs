using FluentValidation;
using Swedating.Entities;
using Swedating.Models;
using System.Linq;

namespace Swedating.Validators
{
	//validate user's name is unique.	
	public class ProfileValidator : AbstractValidator<UserProfile>
	{
		public ProfileValidator()
		{
			RuleFor(x => x.UserName).Must(BeUniqueUrl).WithMessage("Username already exists");			

		}

		private bool BeUniqueUrl(string userName)
		{
			return new ApplicationDbContext().Profiles.FirstOrDefault(x => x.UserName == userName) == null;
		}

	}
}