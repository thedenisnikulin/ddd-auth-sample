using System.Threading.Tasks;
using Chattitude.Api.Entities;
using Chattitude.Api.Dto;
using System.Linq;
using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Chattitude.Api.Services
{
	public class AuthService
	{
		private ILogger<AuthService> _logger { get; }
		private ChattitudeDbContext _dbCtx { get; }
		private IMapper _mapper { get; }
		private TokenService _tokenService { get; }
		public AuthService(
			ILogger<AuthService> logger,
			ChattitudeDbContext dbCtx,
			IMapper mapper,
			TokenService tokenService)
		{
			_logger = logger;
			_dbCtx = dbCtx;
			_mapper = mapper;
			_tokenService = tokenService;
		}
		public UserLoginResponseDto Login(UserLoginDto userLoginDto)
		{
			var userEntity = _mapper.Map<User>(userLoginDto);
			User userFromDb = _dbCtx.Users
				.Where(u => u.Username == userLoginDto.Username)
				.FirstOrDefault();
			if (userFromDb != null 
				&& _comparePasswords(userLoginDto.Password, userFromDb.Password))
			{
				var jwtToken = _tokenService.Generate(userFromDb);
				return new UserLoginResponseDto {
					UserLoginDto = _mapper.Map<UserLoginDto>(userFromDb),
					JwtToken = jwtToken
				};
			}
			return null;
		}
		public async Task<UserRegisterResponseDto> Register(UserRegisterDto userRegisterDto)
		{
			var userEntity = _mapper.Map<User>(userRegisterDto);
			UserRegisterDto newUserRegisterDto;
			User userFromDb = _dbCtx.Users
				.Where(u => u.Username == userRegisterDto.Username)
				.FirstOrDefault();
			if (userFromDb == null)
			{
				string hashedPassword = _hashPassword(userRegisterDto.Password);
				userEntity.Password = hashedPassword;
				_dbCtx.Users.Add(userEntity);
				await _dbCtx.SaveChangesAsync();

				newUserRegisterDto = new UserRegisterDto {
					Username = userRegisterDto.Username,
					Password = hashedPassword,
					Bio = userRegisterDto.Bio
				};
				return new UserRegisterResponseDto {
					UserRegisterDto = newUserRegisterDto,
					JwtToken = _tokenService.Generate(userEntity)
				};
			}
			return null;
		}
		private string _hashPassword(string password)
		{
			byte[] wholeHash = new byte[36];
			byte[] hashedPassword = new byte[20];
			byte[] salt = new byte[16];
			
			using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);

			var pbkdf2 = new Rfc2898DeriveBytes(
				Encoding.ASCII.GetBytes(password),
				salt,
				10000,
				HashAlgorithmName.SHA256
			);
			hashedPassword = pbkdf2.GetBytes(20);
			Array.Copy(hashedPassword, 0, wholeHash, 0, 20);
			Array.Copy(salt, 0, wholeHash, 20, 16);
			return Convert.ToBase64String(wholeHash);
		}
		private bool _comparePasswords(string passwordFromClient, string bas64PpasswordFromDb)
		{
			byte[] hashFromDb = Convert.FromBase64String(bas64PpasswordFromDb);
			byte[] passwordFromDb = new byte[20];
			byte[] saltFromDb = new byte[16];
			Array.Copy(hashFromDb, 20, saltFromDb, 0, 16);

			var pbkdf2 = new Rfc2898DeriveBytes(
				Encoding.ASCII.GetBytes(passwordFromClient),
				saltFromDb,
				10000,
				HashAlgorithmName.SHA256
			);
			Array.Copy(hashFromDb, 0, passwordFromDb, 0, 20);
			byte[] hashedPasswordFromClient = pbkdf2.GetBytes(20);
			return passwordFromDb.SequenceEqual(hashedPasswordFromClient);
		}
	}
}