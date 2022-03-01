using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Configuration;

public class RefreshSessionEntityTypeConfiguration : IEntityTypeConfiguration<RefreshSessionDataModel>
{
	public void Configure(EntityTypeBuilder<RefreshSessionDataModel> builder)
	{
		builder.HasKey(rs => new { rs.UserId, rs.RefreshToken });

		builder.Property(rs => rs.UserId).IsRequired();
		builder.Property(rs => rs.Ip).IsRequired();
		builder.Property(rs => rs.CreatedAt).IsRequired();
		builder.Property(rs => rs.ExpiresAt).IsRequired();

		

		builder
			.HasOne<UserDataModel>()
			.WithMany()
			.HasForeignKey(rs => rs.UserId);
	}
}
