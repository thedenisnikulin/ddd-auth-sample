using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Chattitude.Api.Entities
{
	public class Room
	{
		[Key]
		public Guid Id { get; set; }
		public string Topic { get; set; }

		public List<User> Users { get; set; }
		public List<Message> Messages { get; set; }
	}
}
