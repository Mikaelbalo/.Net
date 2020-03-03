using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swedating.Models
{
	public class UserViewModel
	{
		public string UserName { get; set; }
		public long? ProfileId { get; set; }
		public string Email { get; set; }
		public string PicturePath { get; set; }
	}
}