using Microsoft.IdentityModel.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Infrastructure.Identity.Services;
using Identity.Application.Commands;
using Identity.Application.Contracts;
using Identity.Application.Options;
using Manga.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Manga.Application.Contracts;
using Infrastructure.Manga.Repositories;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Routing;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<AppDbContext>(opts =>
{
	opts.EnableSensitiveDataLogging();
    opts.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

// Identity
var identityBuilder = builder.Services.AddIdentityCore<UserDataModel>();
identityBuilder.AddEntityFrameworkStores<AppDbContext>();
identityBuilder.AddUserManager<UserManager<UserDataModel>>();

// MediatR
builder.Services.AddMediatR(typeof(RegisterUserCommand).Assembly, typeof(PostMangaCommand).Assembly);


// Authentication
builder.Services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(opt =>
	{
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtIssuer:Secret"]));
		opt.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = key,
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidIssuers = new[] { builder.Configuration["JwtIssuer:Issuer"] },
			ValidAudiences= new[] { builder.Configuration["JwtIssuer:Audience"] }
		};

		opt.Events = new JwtBearerEvents
		{
			OnAuthenticationFailed = ctx =>
			{
				ctx.Response.StatusCode = 401;
				ctx.Response.ContentType = "text/plain";
				return ctx.Response.WriteAsync("Authorization failed.");
			}
		};
	});

// Other
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<ITokenFactory, JwtFactory>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IReaderRepository, ReaderRepository>();
builder.Services.AddScoped<IMangaRepository, MangaRepository>();

builder.Services.Configure<JwtIssuerOptions>(
	builder.Configuration.GetSection(JwtIssuerOptions.JwtIssuer));

builder.Services.Configure<RefreshSessionOptions>(
	builder.Configuration.GetSection(RefreshSessionOptions.RefreshSession));

builder.Services.AddAutoMapper(typeof(Infrastructure.Data.MappingProfile).Assembly);

var app = builder.Build();

IdentityModelEventSource.ShowPII = true;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("spec");

app.Use(async (ctx, n) =>
{
	var endp = ctx.GetEndpoint();
	var rendp = (RouteEndpoint) endp;
	Console.WriteLine("");
	await n();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
