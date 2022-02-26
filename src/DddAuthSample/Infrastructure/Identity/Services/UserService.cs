using Identity.Application.Contracts;
using Identity.Domain.Entities;
using Infrastructure.Data.Models;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using SharedKernel;

namespace Infrastructure.Identity.Services;

public class UserService : IUserService
{
	private readonly AppDbContext _context;
	private readonly UserManager<AppUser> _userManager;
	private readonly IMapper _mapper;

	public UserService(
		AppDbContext context,
		UserManager<AppUser> userManager,
		IMapper mapper)
	{
		_context = context;
		_userManager = userManager;
		_mapper = mapper;
	}

	public Task Create(User user)
	{
		var appUser = _mapper.Map<AppUser>(user);
		return _userManager.CreateAsync(appUser, appUser.PasswordHash);
	}

	public Task Update(User user)
	{
		var appUser = _mapper.Map<AppUser>(user);
		return _userManager.UpdateAsync(appUser);
	}

	public Task<User> GetById(UserId userId)
	{
		return _userManager.FindByIdAsync(userId.Value.ToString())
			.ContinueWith(appUser => _mapper.Map<User>(appUser));
	}

	public Task<User> GetByName(string name)
	{
		return _userManager.FindByNameAsync(name)
			.ContinueWith(appUser => _mapper.Map<User>(appUser));
	}
}
