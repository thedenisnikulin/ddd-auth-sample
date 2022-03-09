using Xunit;
using Moq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Infrastructure.Data;
using Infrastructure.Identity.Services;
using Identity.Domain.Entities;
using System;
using Infrastructure.Data.Models;

namespace DddAuthSample.UnitTests.ServiceTests;

public class UserServiceTest
{
	// only one method is tested just for the sake of demonstration
	// [Fact]
	// public void Create_WhenCalled_ReturnsUser()
	// {
	// 	// arrange
	// 	var options = new DbContextOptionsBuilder<AppDbContext>()
	// 		.UseInMemoryDatabase("MangaDb")
	// 		.Options;
	//
	// 	var context = new AppDbContext(options);
	//
	// }
	//
	// private UserService _createUserService()
	// {
	// 	var options = new DbContextOptionsBuilder<AppDbContext>()
	// 		.UseInMemoryDatabase("MangaDb")
	// 		.Options;
	//
	// 	var context = new AppDbContext(options);
	// 
	// 	var config = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
	// }
	//
	// private UserManager
	
	[Fact]
	public void Map_UserToUserDataModel()
	{
		// arrange
		var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
		var mapper = config.CreateMapper();
		var userDataModel = new UserDataModel
		{
			Id = Guid.NewGuid(),
			UserName = "denis",
			PasswordHash = "safdkfjd;gdsfgdsfg"
		};

		// act
		var user = mapper.Map<User>(userDataModel);
		System.Console.WriteLine(user.Id.Value.ToString());

		// assert
		Assert.NotStrictEqual(user.Id.Value, Guid.Empty);
		Assert.Equal(user.Name, userDataModel.UserName);
		Assert.Equal(user.HashedPassword, userDataModel.PasswordHash);
	}
}
