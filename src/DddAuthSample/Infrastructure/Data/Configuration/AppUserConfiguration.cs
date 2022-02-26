using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedKernel;
using Infrastructure.Data.Models;
using Identity.Domain.Entities;

namespace Infrastructure.Data.Configuration;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
	public void Configure(EntityTypeBuilder<AppUser> builder)
	{
		builder
			.HasMany(au => au.RefreshSessions)
			.WithOne();
	}
}
