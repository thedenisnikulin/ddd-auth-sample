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
using Infrastructure.Manga.Repositories;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using SharedKernel;

namespace DddAuthSample.IntegrationTests.ServiceTests;

using DomainManga = global::Manga.Domain.Entities.Manga;

public class UserServiceTest
{
	private (AppDbContext, UserService) _prepare()
	{
		var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase("MangaDb")
			.Options;

		var context = new AppDbContext(dbOptions);
		var userStore = new UserStore<UserDataModel, IdentityRole<Guid>, AppDbContext, Guid>(context);
		var userManager = new UserManager<UserDataModel>(userStore, null, new PasswordHasher<UserDataModel>(), null, null, null, null, null, null);
		var mapperConf = new MapperConfiguration(opt => opt.AddProfile(new MappingProfile()));
		var mapper = new Mapper(mapperConf);
		var userService = new UserService(context, userManager, mapper);
		return (context, userService);
	}

	[Fact]
	public async Task Create_CreatesUser()
	{
		var (context, userService) = _prepare();
		// Arrange
		var domainUser = User.Create("denis", "secret=F2");

		// Act
		await userService.Create(domainUser);

		// Assert
		var userFromDb = await context.Users.FirstAsync(u => u.UserName == "denis");
		Assert.NotNull(userFromDb);
		Assert.Equal(domainUser.Name, userFromDb.UserName);
		Assert.NotNull(userFromDb.PasswordHash);
		Assert.NotEqual(userFromDb.Id, Guid.Empty);
	}

	[Fact]
	public async Task Update_UpdatesUser()
	{
		// Arrange 
		var (context, userService) = _prepare();
		var domainUser = User.Create("denis", "secret=F2");
		await userService.Create(domainUser);
		var userFromDb = await userService.GetByName("denis");
		userFromDb.AddRefreshSession(
			RefreshSession.Create(
				userFromDb.Id,
				"blablabla",
				"blablabla",
				new DateTime().AddDays(30)));

		// Act
		await userService.Update(userFromDb);

		// Assert 
		var updatedUser = await userService.GetByName("denis");
		Assert.NotNull(updatedUser);
		Assert.NotNull(updatedUser.RefreshSessions);
		Assert.NotEmpty(updatedUser.RefreshSessions);
	}

	[Fact]
	public async Task GetById_ReturnsUser()
	{
		// Arrange
		var (context, userService) = _prepare();
		var domainUser = User.Create("denis", "secret=F2");
		await userService.Create(domainUser);
		var id = (await context.Users.ToListAsync())[0].Id;
		var userId = new UserId(id);

		// Act 
		var userFromDb = await userService.GetById(userId);

		// Assert 
		Assert.NotNull(userFromDb);
		Assert.Equal(domainUser.Name, userFromDb.Name);
	}

	[Fact]
	public async Task GetByName_ReturnsUser()
	{
		// Arrange
		var (context, userService) = _prepare();
		var domainUser = User.Create("denis", "secret=F2");
		await userService.Create(domainUser);

		// Act 
		var userFromDb = await userService.GetByName(domainUser.Name);

		// Assert 
		Assert.NotNull(userFromDb);
		Assert.Equal(domainUser.Name, userFromDb.Name);
	}

	[Fact]
	public async Task VerifyPassword_ReturnsCorrectResult()
	{
		// Arrange
		var (context, userService) = _prepare();
		var domainUser = User.Create("denis", "secret=F2");
		await userService.Create(domainUser);
		var userFromDb = await userService.GetByName("denis");
		var passwordCorrect = "secret=F2";
		var passwordIncorrect = "whaaat";
		
		// Act
		var mustBeTrue = await userService.VerifyPassword(userFromDb, passwordCorrect);
		var mustBeFalse = await userService.VerifyPassword(userFromDb, passwordIncorrect);

		// Assert
		Assert.True(mustBeTrue);
		Assert.False(mustBeFalse);
	}
}
