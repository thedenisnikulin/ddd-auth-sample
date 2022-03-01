using AutoMapper;
using Identity.Domain.Entities;
using Infrastructure.Data.Models;
using Manga.Domain.Entities;
using SharedKernel;
using DomainManga = Manga.Domain.Entities.Manga;

namespace Infrastructure.Data;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		// TypedId <-> Id mapping. 
		// (perhaps there's some generic way to map these like TypedId -> Guid 
		// but i'm quite lazy to dig into details)
		CreateMap<UserId, Guid>()
			.ForMember(
				id => new UserId(id),
				opt => opt.MapFrom(typedId => typedId.Value))
			.ReverseMap();
		CreateMap<AuthorId, Guid>()
			.ForMember(
				id => new AuthorId(id),
				opt => opt.MapFrom(typedId => typedId.Value))
			.ReverseMap();
		CreateMap<ReaderId, Guid>()
			.ForMember(
				id => new ReaderId(id),
				opt => opt.MapFrom(typedId => typedId.Value))
			.ReverseMap();
		CreateMap<MangaId, Guid>()
			.ForMember(
				id => new MangaId(id),
				opt => opt.MapFrom(typedId => typedId.Value))
			.ReverseMap();

		// Entity <-> DataModel mapping.
		CreateMap<User, UserDataModel>()
			.ForMember(
				userDataModel => userDataModel.UserName,
				opt => opt.MapFrom(user => user.Name))
			.ForMember(
				userDataModel => userDataModel.PasswordHash,
				opt => opt.MapFrom(user => user.HashedPassword))
			.ReverseMap();

		CreateMap<Author, AuthorDataModel>().ReverseMap();
		CreateMap<Reader, ReaderDataModel>().ReverseMap();
		CreateMap<RefreshSession, RefreshSessionDataModel>().ReverseMap();
		CreateMap<DomainManga, MangaDataModel>().ReverseMap();
		CreateMap<BookmarkedManga, BookmarkedMangaDataModel>()
			.ForMember(
				bmDataModel => Enum.Parse<Bookmark>(bmDataModel.Bookmark),
				opt => opt.MapFrom(bm => bm.Bookmark.ToString()))
			.ReverseMap();
	}
}
