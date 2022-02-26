using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Manga.Domain.Entities;
using DomainManga = Manga.Domain.Entities.Manga;

namespace Infrastructure.Data.Configuration;

public class MangaConfiguration : IEntityTypeConfiguration<DomainManga>
{
	public void Configure(EntityTypeBuilder<DomainManga> builder)
	{
		var mangaIdConverter = new ValueConverter<MangaId, Guid>(
			m => m.Value,
			p => new MangaId(p));

		var authorIdConverter = new ValueConverter<AuthorId, Guid>(
			m => m.Value,
			p => new AuthorId(p));

		builder.HasKey(m => m.Id);
		builder.Property(m => m.Title).IsRequired();
		builder.Property(m => m.AuthorId).IsRequired();

		builder.Property(m => m.Id).HasConversion(mangaIdConverter);
		builder.Property(m => m.AuthorId).HasConversion(authorIdConverter);

		builder
			.HasOne<Author>()
			.WithMany()
			.HasForeignKey(m => m.AuthorId);
	}
}
