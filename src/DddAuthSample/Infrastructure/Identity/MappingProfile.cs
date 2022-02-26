using AutoMapper;
using Identity.Domain.Entities;
using Infrastructure.Data.Models;

namespace Infrastructure.Identity;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<User, AppUser>()
			.ForMember(
				appUser => appUser.Id,
				opt => opt.MapFrom(user => user.Id.Value))
			.ForMember(
				appUser => appUser.UserName,
				opt => opt.MapFrom(user => user.Name))
			.ForMember(
				appUser => appUser.PasswordHash,
				opt => opt.MapFrom(user => user.HashedPassword))
			.ForMember(
				appUser => appUser.RefreshSessions,
				opt => opt.MapFrom(user => user.RefreshSessions))
			.ReverseMap();
	}
}
