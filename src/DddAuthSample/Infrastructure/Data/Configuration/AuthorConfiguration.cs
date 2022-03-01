using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Configuration;

public class AuthorConfiguration : IEntityTypeConfiguration<AuthorDataModel>
{
	public void Configure(EntityTypeBuilder<AuthorDataModel> builder)
	{
		builder.HasKey(a => a.Id);
		builder.Property(a => a.UserId).IsRequired();

		builder
			.HasOne<UserDataModel>()
			.WithOne()
			.HasForeignKey<AuthorDataModel>(a => a.UserId);

		builder
			.HasMany(a => a.PublishedManga)
			.WithOne();
	}
}
