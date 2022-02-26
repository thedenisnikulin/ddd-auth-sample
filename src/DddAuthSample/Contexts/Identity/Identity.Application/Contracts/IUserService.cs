using Identity.Domain.Entities;
using SharedKernel;

namespace Identity.Application.Contracts;

public interface IUserService
{
	Task Create(User user);
	Task Update(User user);
	Task<User> GetById(UserId userId);
	Task<User> GetByName(string name);
}
