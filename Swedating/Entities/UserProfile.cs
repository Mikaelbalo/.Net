using Swedating.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Swedating.Entities
{
    [Table("swedating_user_profile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]          
        public string UserId { get; set; }    

        [Column("username")]
        [Required]
        [DisplayName("Username")]
        [StringLength(50)]
        [MinLength(3)]
        public string UserName { get; set; }
        
        [Column("first_name")]
        [Required]
        [DisplayName("First name")]
        [StringLength(50)]
        [MinLength(3)]
        public string FirstName { get; set; }
        
        [Column("last_name")]
        [Required]
        [DisplayName("Last name")]
        [StringLength(50)]
        [MinLength(3)]
        public string LastName { get; set; }

        [Column("email")]           
        public string Email { get; set; }

        [Column("gender_id")]
        [Required]
        [DisplayName("Gender")]
        public int GenderId { get; set; }

        [NotMapped]
        public GenderEnum Gender { get; set; }

     
        [Column("birth_date")]
        [Required]
        [DisplayName("Birth date")]
        public DateTime BirthDate { get; set; }

        [Column("created_date")]
        [Required]
        [DisplayName("Date created")]
        public DateTime CreatedDate { get; set; }

        [Column("picture_path")]           
        public string PicturePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase PictureFile { get; set; }
      
        public virtual List<FriendRelationshiop> FriendProfiles { get; set; }
        public virtual List<ProfilePost> Posts { get; set; }

     
    }
}