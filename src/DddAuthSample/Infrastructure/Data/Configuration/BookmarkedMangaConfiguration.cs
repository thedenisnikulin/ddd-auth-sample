using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Manga.Domain.Entities;
using DomainManga = Manga.Domain.Entities.Manga;

namespace Infrastructure.Data.Configuration;

public class BookmarkedMangaConfiguration : IEntityTypeConfiguration<BookmarkedManga>
{
	public void Configure(EntityTypeBuilder<BookmarkedManga> builder)
	{
		var mangaIdConverter = new ValueConverter<MangaId, Guid>(
			m => m.Value,
			p => new MangaId(p));

		var readerIdConverter = new ValueConverter<ReaderId, Guid>(
			m => m.Value,
			p => new ReaderId(p));

		var bookmarkConverter = new ValueConverter<Bookmark, string>(
			m => m.ToString(),
			p => Enum.Parse<Bookmark>(p)
		);

		builder.HasKey(bm => new { bm.ReaderId, bm.MangaId });

		builder.Property(bm => bm.ReaderId).IsRequired();

		builder.Property(bm => bm.Bookmark).HasConversion(bookmarkConverter);
		builder.Property(bm => bm.ReaderId).HasConversion(readerIdConverter);

		builder
			.HasOne<DomainManga>()
			.WithOne()
			.HasForeignKey<BookmarkedManga>(bm => bm.MangaId);
	}
}
