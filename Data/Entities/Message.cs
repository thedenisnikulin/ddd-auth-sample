using System;

namespace Chattitude.Api.Entities
{
	public class Message
	{
		public Guid Id { get; set;  }
		public string Text { get; set; }
		public Guid SenderId { get; set; }
		public User Sender { get; set; }
		public Room Room { get; set; }
		public Guid RoomId { get; set; }
	}
}
