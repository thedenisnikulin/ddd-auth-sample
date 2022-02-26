using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Manga.Domain.Entities;
using DomainManga = Manga.Domain.Entities.Manga;
using SharedKernel;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Configuration;

public class ReaderConfiguration : IEntityTypeConfiguration<Reader>
{
	public void Configure(EntityTypeBuilder<Reader> builder)
	{
		var readerIdConverter = new ValueConverter<ReaderId, Guid>(
			m => m.Value,
			p => new ReaderId(p));
		
		var userIdConverter = new ValueConverter<UserId, Guid>(
			m => m.Value,
			p => new UserId(p));

		builder.HasKey(r => r.Id);
		builder.Property(r => r.UserId).IsRequired();

		builder.Property(r => r.Id).HasConversion(readerIdConverter);
		builder.Property(r => r.UserId).HasConversion(userIdConverter);

		builder
			.HasOne<AppUser>()
			.WithOne()
			.HasForeignKey<Reader>(r => r.UserId);

		builder
			.HasMany(r => r.BookmarkedManga)
			.WithOne();
	}
}
