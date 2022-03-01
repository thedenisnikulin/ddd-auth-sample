using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Configuration;

public class AppUserConfiguration : IEntityTypeConfiguration<UserDataModel>
{
	public void Configure(EntityTypeBuilder<UserDataModel> builder)
	{
		builder
			.HasMany(u => u.RefreshSessions)
			.WithOne();
	}
}
