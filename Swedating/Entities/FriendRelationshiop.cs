using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Swedating.Entities
{
	//Relates a profile with other friend profiles.
	[Table("swedating_friend_relationship")]
	public class FriendRelationshiop
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

	



	}
}