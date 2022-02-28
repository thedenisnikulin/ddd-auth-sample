using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Data.Models;
using SharedKernel;

namespace Infrastructure.Data;

public class AppDbContext : IdentityDbContext<UserDataModel, IdentityRole<Guid>, Guid>
{
	public DbSet<MangaDataModel> Manga { get; set; }
	public DbSet<BookmarkedMangaDataModel> BookmarkedManga { get; set; }
	public DbSet<AuthorDataModel> Authors { get; set; }
	public DbSet<ReaderDataModel> Readers { get; set; }
	public DbSet<RefreshSessionDataModel> RefreshSessions { get; set; }

	public AppDbContext(DbContextOptions options) : base(options) {}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
	}
}
