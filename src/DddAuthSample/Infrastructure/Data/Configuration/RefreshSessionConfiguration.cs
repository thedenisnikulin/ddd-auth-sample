using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Identity.Domain.Entities;
using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedKernel;

namespace Infrastructure.Data.Configuration;

public class RefreshSessionEntityTypeConfiguration : IEntityTypeConfiguration<RefreshSession>
{
	public void Configure(EntityTypeBuilder<RefreshSession> builder)
	{
		var userIdConverter = new ValueConverter<UserId, Guid>(
			m => m.Value,
			p => new UserId(p));

		builder.HasKey(rs => new { rs.UserId, rs.RefreshToken });

		builder.Property(rs => rs.UserId).HasConversion(userIdConverter);
		builder.Property(rs => rs.Ip).IsRequired();
		builder.Property(rs => rs.CreatedAt).IsRequired();
		builder.Property(rs => rs.ExpiresAt).IsRequired();

		

		builder
			.HasOne<AppUser>()
			.WithMany()
			.HasForeignKey(rs => rs.UserId);
	}
}
