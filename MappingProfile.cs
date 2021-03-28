using AutoMapper;
using Chattitude.Api.Entities;
using System;

namespace Chattitude.Api.Dto
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserDto>();
			CreateMap<UserDto, User>()
				.ForMember(u => u.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

			CreateMap<User, UserLoginDto>();
			CreateMap<UserLoginDto, User>()
				.ForMember(u => u.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

			CreateMap<User, UserRegisterDto>();
			CreateMap<UserRegisterDto, User>()
				.ForMember(u => u.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
		}
	}
}
