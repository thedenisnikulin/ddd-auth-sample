using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DomainManga = Manga.Domain.Entities.Manga;
using Identity.Domain.Entities;
using Manga.Domain.Entities;
using Infrastructure.Data.Models;
using SharedKernel;

namespace Infrastructure.Data;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
	public DbSet<DomainManga> Manga { get; set; }
	public DbSet<BookmarkedManga> BookmarkedManga { get; set; }
	public DbSet<Author> Authors { get; set; }
	public DbSet<Reader> Readers { get; set; }
	public DbSet<RefreshSession> RefreshSessions { get; set; }

	public AppDbContext(DbContextOptions options) : base(options) {}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
	}
}
