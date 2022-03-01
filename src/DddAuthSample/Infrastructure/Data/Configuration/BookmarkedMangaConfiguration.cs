using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Configuration;

public class BookmarkedMangaConfiguration : IEntityTypeConfiguration<BookmarkedMangaDataModel>
{
	public void Configure(EntityTypeBuilder<BookmarkedMangaDataModel> builder)
	{
		builder.HasKey(bm => new { bm.ReaderId, bm.MangaId });

		builder.Property(bm => bm.ReaderId).IsRequired();

		builder.Property(bm => bm.Bookmark).IsRequired();
		builder.Property(bm => bm.ReaderId).IsRequired();

		builder
			.HasOne<MangaDataModel>()
			.WithOne()
			.HasForeignKey<BookmarkedMangaDataModel>(bm => bm.MangaId);
	}
}
