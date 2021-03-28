using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Chattitude.Api.Entities
{
	public class User
	{
		[Key]
		[Required]
		public Guid Id { get; set; }
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		public string Bio { get; set; }
		[Required]
		public int Rep { get; set; }
		[Required]
		public bool IsSearching { get; set; }
		public Guid? RoomId { get; set; }
		public Room Room { get; set; }
		public List<Message> Messages { get; set; }
	}
}
