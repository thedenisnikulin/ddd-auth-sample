using Microsoft.AspNetCore.Identity;
using SharedKernel;
using Identity.Domain.Entities;

namespace Infrastructure.Data.Models;

// Persistance representation of domain User
// (the idea is to stick with ASP.NET Identity framework,
// but don't violate domain boundaries)
public class AppUser : IdentityUser<Guid>
{
	public List<RefreshSession> RefreshSessions { get; set; }
}
