using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Identity.Domain.Entities;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Configuration;

public class RefreshSessionEntityTypeConfiguration : IEntityTypeConfiguration<RefreshSession>
{
	public void Configure(EntityTypeBuilder<RefreshSession> builder)
	{
		builder
			.HasAlternateKey(rs => rs.RefreshToken);

		builder.Property(rs => rs.Ip).IsRequired();
		builder.Property(rs => rs.CreatedAt).IsRequired();
		builder.Property(rs => rs.ExpiresAt).IsRequired();

		builder
			.HasOne<AppUser>()
			.WithMany()
			.HasForeignKey(rs => rs.UserId);
	}
}
