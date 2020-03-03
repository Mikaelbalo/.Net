using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swedating.Entities
{
	//Relates the profile with the posts on the wall.
	[Table("swedating_profile_posts")]
	public class ProfilePost
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("id")]
		public int Id { get; set; }

		[Column("user_profile_id")]
		[ForeignKey("UserProfile")]
		[Required]
		public int UserProfileId { get; set; }
		public virtual UserProfile UserProfile { get; set; }

		[Column("friend_user_profile_id")]
		[ForeignKey("FriendUserProfile")]
		[Required]
		public int FriendUserProfileId { get; set; }
		public virtual UserProfile FriendUserProfile { get; set; }

		[Column("date")]
		[Required]
		public DateTime Date { get; set; }

		[Column("message")]
		[Required]
		public string Message { get; set; }


	}
}