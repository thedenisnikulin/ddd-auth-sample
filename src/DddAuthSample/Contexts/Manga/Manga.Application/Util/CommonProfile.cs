using AutoMapper;
using Manga.Application.Messages;

namespace Manga.Application.Util;

using Manga = Manga.Domain.Entities.Manga;

public class CommonProfile : Profile
{
	public CommonProfile()
	{
		CreateMap<Manga, MangaDto>()
			.ForMember(m => m.MangaId, opt => opt.MapFrom(m => m.Id.Value.ToString()))
			.ForMember(m => m.AuthorId, opt => opt.MapFrom(m => m.AuthorId.Value.ToString()))
			.ReverseMap();
	}
}
