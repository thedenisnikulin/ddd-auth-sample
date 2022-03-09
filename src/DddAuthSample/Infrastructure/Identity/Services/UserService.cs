using Identity.Application.Contracts;
using Identity.Domain.Entities;
using Infrastructure.Data.Models;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using SharedKernel;
using Microsoft.EntityFrameworkCore;

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

	public async Task Update(User user)
	{
		var updatedUserDataModel = _mapper.Map<UserDataModel>(user);
		var trackedUserDataModel = _context.Users.Find(user.Id.Value);

		_context.Entry(trackedUserDataModel).OriginalValues.SetValues(updatedUserDataModel);

		await _userManager.UpdateSecurityStampAsync(trackedUserDataModel);
		trackedUserDataModel.RefreshSessions = updatedUserDataModel.RefreshSessions;

		await _userManager.UpdateAsync(trackedUserDataModel);
	}

	public Task<User> GetById(UserId userId)
	{
		var userDataModel = _context.Users
			.FirstOrDefault(u => u.Id == userId.Value);
		return Task.FromResult(_mapper.Map<User>(userDataModel));
	}

	public Task<User> GetByName(string name)
	{
		var userDataModel = _context.Users
			.FirstOrDefault(u => u.UserName == name);
		return Task.FromResult(_mapper.Map<User>(userDataModel));
	}

	public Task<bool> VerifyPassword(User user, string password)
	{
		var userDataModel = _mapper.Map<UserDataModel>(user);
		var verificationResult = _userManager.PasswordHasher.VerifyHashedPassword(userDataModel, userDataModel.PasswordHash, password);

		return Task.FromResult(verificationResult switch
		{
			PasswordVerificationResult.Success => true,
			PasswordVerificationResult.SuccessRehashNeeded => true,
			PasswordVerificationResult.Failed => false,
			_ => false
		});
	}
}
