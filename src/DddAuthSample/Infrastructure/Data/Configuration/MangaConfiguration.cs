using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Configuration;

public class MangaConfiguration : IEntityTypeConfiguration<MangaDataModel>
{
	public void Configure(EntityTypeBuilder<MangaDataModel> builder)
	{
		builder.HasKey(m => m.Id);
		builder.Property(m => m.Title).IsRequired();
		builder.Property(m => m.AuthorId).IsRequired();

		builder
			.HasOne<AuthorDataModel>()
			.WithMany()
			.HasForeignKey(m => m.AuthorId);
	}
}
