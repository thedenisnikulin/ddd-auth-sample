using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Manga.Domain.Entities;
using DomainManga = Manga.Domain.Entities.Manga;
using SharedKernel;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Configuration;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
	public void Configure(EntityTypeBuilder<Author> builder)
	{
		var authorIdConverter = new ValueConverter<AuthorId, Guid>(
			m => m.Value,
			p => new AuthorId(p));
		
		var userIdConverter = new ValueConverter<UserId, Guid>(
			m => m.Value,
			p => new UserId(p));

		builder.HasKey(a => a.Id);
		builder.Property(a => a.UserId).IsRequired();

		builder.Property(a => a.Id).HasConversion(authorIdConverter);
		builder.Property(a => a.UserId).HasConversion(userIdConverter);

		builder
			.HasOne<AppUser>()
			.WithOne()
			.HasForeignKey<Author>(a => a.UserId);

		builder
			.HasMany(a => a.PublishedManga)
			.WithOne();
	}
}
