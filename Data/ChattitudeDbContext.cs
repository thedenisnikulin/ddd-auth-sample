using Microsoft.EntityFrameworkCore;

namespace Chattitude.Api.Entities
{
	public class ChattitudeDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Room> Rooms { get; set; }
		public DbSet<Message> Messages { get; set; }

		public ChattitudeDbContext(DbContextOptions<ChattitudeDbContext> options)
			:base(options) 
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<User>()
				.HasIndex(u => u.Username)
				.IsUnique();

			builder.Entity<User>()
				.Property(u => u.Rep)
				.HasDefaultValue(1);

			builder.Entity<User>()
				.HasOne(u => u.Room)
				.WithMany(r => r.Users)
				.HasForeignKey(u => u.RoomId)
				.IsRequired(false);

			builder.Entity<User>()
				.HasMany(u => u.Messages)
				.WithOne(m => m.Sender)
				.IsRequired(false);

			builder.Entity<Room>()
				.HasMany(r => r.Users);

			builder.Entity<Room>()
				.HasMany(r => r.Messages)
				.WithOne()
				.IsRequired(false);

			builder.Entity<Message>()
				.HasOne(m => m.Sender)
				.WithMany(s => s.Messages)
				.HasForeignKey(m => m.SenderId);

			builder.Entity<Message>()
				.HasOne(m => m.Room)
				.WithMany(r => r.Messages);
		}
	}
}
