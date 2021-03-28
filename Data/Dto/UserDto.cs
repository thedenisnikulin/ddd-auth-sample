using System;
using System.Collections.Generic;
using Chattitude.Api.Entities;

namespace Chattitude.Api.Dto
{
	public class UserDto
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Bio { get; set; }
		public int Rep { get; set; }
		public bool IsSearching { get; set; }
		public Guid? RoomId { get; set; }
	}
	public class UserLoginDto
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
	public class UserRegisterDto
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Bio { get; set; }
	}
	public class UserLoginResponseDto
	{
		public UserLoginDto UserLoginDto { get; set; }
		public string JwtToken { get; set; }
	}
	public class UserRegisterResponseDto
	{
		public UserRegisterDto UserRegisterDto { get; set; }
		public string JwtToken { get; set; }
	}
}