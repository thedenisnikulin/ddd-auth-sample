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
	private readonly UserManager<UserDataModel> _userManager;
	private readonly IMapper _mapper;

	public UserService(
		AppDbContext context,
		UserManager<UserDataModel> userManager,
		IMapper mapper)
	{
		_context = context;
		_userManager = userManager;
		_mapper = mapper;
	}

	public Task Create(User user)
	{
		var userDataModel = _mapper.Map<UserDataModel>(user);
		return _userManager.CreateAsync(userDataModel, userDataModel.PasswordHash);
	}

	public Task Update(User user)
	{
		var userDataModel = _mapper.Map<UserDataModel>(user);
		return _userManager.UpdateAsync(userDataModel);
	}

	public Task<User> GetById(UserId userId)
	{
		return _userManager.FindByIdAsync(userId.Value.ToString())
			.ContinueWith(userDataModel => _mapper.Map<User>(userDataModel));
	}

	public Task<User> GetByName(string name)
	{
		return _userManager.FindByNameAsync(name)
			.ContinueWith(userDataModel => _mapper.Map<User>(userDataModel.Result));
	}
}
