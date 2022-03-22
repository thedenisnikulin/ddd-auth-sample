using AutoMapper;
using Identity.Domain.Entities;
using Infrastructure.Data.Models;
using Manga.Domain.Entities;
using SharedKernel;
using DomainManga = Manga.Domain.Entities.Manga;

namespace Infrastructure.Data;

public class UserIdConverter : IValueConverter<Guid, UserId> {
    public UserId Convert(Guid source, ResolutionContext context)
        => new UserId(source);
}

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		// TypedId <-> Guid mapping. 
		// (perhaps there's some generic way to map these like TypedId -> Guid 
		// but i'm quite lazy to dig into details)
		CreateMap<UserId, Guid>().ConvertUsing(s => s.Value);
		CreateMap<Guid, UserId>().ConvertUsing(s => new UserId(s));
		
		CreateMap<AuthorId, Guid>().ConvertUsing(s => s.Value);
		CreateMap<Guid, AuthorId>().ConvertUsing(s => new AuthorId(s));
	
		CreateMap<ReaderId, Guid>().ConvertUsing(s => s.Value);
		CreateMap<Guid, ReaderId>().ConvertUsing(s => new ReaderId(s));

		CreateMap<MangaId, Guid>().ConvertUsing(s => s.Value);
		CreateMap<Guid, MangaId>().ConvertUsing(s => new MangaId(s));

		CreateMap<Bookmark, string>().ConvertUsing(s => s.ToString());
		CreateMap<string, Bookmark>().ConvertUsing(s => Enum.Parse<Bookmark>(s));

		CreateMap<DateTime, DateTime>().ConvertUsing(d => d.ToUniversalTime());

		// Entity <-> DataModel mapping.
		CreateMap<UserDataModel, User>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(s => new UserId(s.Id)))
			.ForMember(
				dest => dest.Name,
				opt => opt.MapFrom(src => src.UserName))
			.ForMember(
				dest => dest.HashedPassword,
				opt => opt.MapFrom(src => src.PasswordHash))
			.ForMember(
				dest => dest.RefreshSessions,
				opt => opt.MapFrom(src => src.RefreshSessions == null 
					? new List<RefreshSessionDataModel>().AsReadOnly()
					: src.RefreshSessions.AsReadOnly()))
			.ReverseMap();

		CreateMap<AuthorDataModel, Author>()
			.ForMember(
				dest => dest.PublishedManga,
				opt => opt.MapFrom(src =>
					src.PublishedManga == null
					? new List<MangaDataModel>().AsReadOnly()
					: src.PublishedManga.AsReadOnly()))
			.ReverseMap();
		CreateMap<ReaderDataModel, Reader>()
			.ForMember(
				dest => dest.BookmarkedManga,
				opt => opt.MapFrom(src =>
					src.BookmarkedManga == null
					? new List<BookmarkedMangaDataModel>().AsReadOnly()
					: src.BookmarkedManga.AsReadOnly()))
			.ReverseMap();
		CreateMap<RefreshSessionDataModel, RefreshSession>().ReverseMap();
		CreateMap<MangaDataModel, DomainManga>().ReverseMap();
		CreateMap<BookmarkedManga, BookmarkedMangaDataModel>().ReverseMap();

	}
}
