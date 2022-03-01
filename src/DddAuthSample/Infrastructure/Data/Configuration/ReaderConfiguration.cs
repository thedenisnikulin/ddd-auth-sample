using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Configuration;

public class ReaderConfiguration : IEntityTypeConfiguration<ReaderDataModel>
{
	public void Configure(EntityTypeBuilder<ReaderDataModel> builder)
	{
		builder.HasKey(r => r.Id);
		builder.Property(r => r.UserId).IsRequired();

		builder.Property(r => r.Id).IsRequired();
		builder.Property(r => r.UserId).IsRequired();

		builder
			.HasOne<UserDataModel>()
			.WithOne()
			.HasForeignKey<ReaderDataModel>(r => r.UserId);

		builder
			.HasMany(r => r.BookmarkedManga)
			.WithOne();
	}
}
