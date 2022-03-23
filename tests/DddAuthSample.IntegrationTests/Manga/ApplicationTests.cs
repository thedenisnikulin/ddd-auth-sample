using Xunit;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using System;
using Infrastructure.Manga.Repositories;
using System.Threading.Tasks;
using Manga.Application.Commands;
using System.Threading;
using System.Linq;

namespace DddAuthSample.IntegrationTests.Manga;

public class ApplicationTests
{
	private (AppDbContext, Mapper) _arrangeCommonDependencies()
	{
		var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase("MangaDb " + Guid.NewGuid().ToString())
			.Options;

		var context = new AppDbContext(dbOptions);
		var mapperConf = new MapperConfiguration(o => o.AddProfile(new MappingProfile()));
		var mapper = new Mapper(mapperConf);
		return (context, mapper);
	}

	[Fact]
	public async Task CreateAuthor_CreatesAuthor()
	{
		// Arrange
		var (context, mapper) = _arrangeCommonDependencies();
		var repo = new AuthorRepository(context, mapper);
		var handler = new CreateAuthorCommand.CreateAuthorCommandHandler(repo);
		var command = new CreateAuthorCommand { UserId = Guid.NewGuid() };

		// Act
		var authorId = await handler.Handle(command, new CancellationToken());

		// Assert
		Assert.NotEmpty(await context.Authors.ToListAsync());
		Assert.NotNull(authorId);
		Assert.NotEqual(authorId.Value, Guid.Empty);
		Assert.NotNull(await context.Authors.FirstAsync(a => a.Id == authorId.Value));
	}

	[Fact]
	public async Task CreateReader_CreatesReader()
	{
		// Arrange
		var (context, mapper) = _arrangeCommonDependencies();
		var repo = new ReaderRepository(context, mapper);
		var handler = new CreateReaderCommand.CreateReaderCommandHandler(repo);
		var command = new CreateReaderCommand { UserId = Guid.NewGuid() };

		// Act
		var readerId = await handler.Handle(command, new CancellationToken());

		// Assert
		Assert.NotEmpty(await context.Readers.ToListAsync());
		Assert.NotNull(readerId);
		Assert.NotEqual(readerId.Value, Guid.Empty);
		Assert.NotNull(await context.Readers.FirstAsync(a => a.Id == readerId.Value));
	}

	[Fact]
	public async Task PostManga_SavesMangaToDatabase()
	{
		// Arrange 
		var (context, mapper) = _arrangeCommonDependencies();
		var authorId = Guid.NewGuid();

		System.Console.WriteLine(context.Authors.ToList().Aggregate("before add: ", (acc, el) => acc += $"{el.Id.ToString()}, "));
		context.Authors.Add(new Infrastructure.Data.Models.AuthorDataModel
		{
			Id = authorId,
			UserId = Guid.Empty,
			PublishedManga = new()
		});
		context.SaveChanges();

		System.Console.WriteLine(context.Authors.ToList().Aggregate("after save: ", (acc, el) => acc += $"{el.Id.ToString()}, "));

		var repo = new AuthorRepository(context, mapper);
		var handler = new PostMangaCommand.PostMangaCommandHandler(repo);
		var command = new PostMangaCommand { AuthorId = authorId, Title = "title" };
		
		// Act 
		_ = await handler.Handle(command, new CancellationToken());

		// Assert 
		Assert.NotEmpty(context.Manga.ToList());
		Assert.NotEmpty(context.Authors.Find(authorId).PublishedManga);
		Assert.Equal(context.Manga.ToList().Last().Title, command.Title);
		Assert.Equal(context.Manga.ToList().Last().AuthorId, command.AuthorId);
	}
}
