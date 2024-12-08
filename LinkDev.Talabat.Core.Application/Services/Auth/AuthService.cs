using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Auth.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Common;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Application.Extensions;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Auth
{
	public class AuthService(IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthService
	{
		public async Task<UserDto> LoginAsync(LoginDto model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);
			if (user == null)
				throw new BadRequestException("Invalid Login");

			var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);

			//if (!result.Succeeded)
			//	throw new UnAthorizedException("Invalid Login");

			var response = new UserDto()
			{
				Id = user.Id,
				DisplayName = user.DisplayName,
				Email = user.Email!,
				Token = await GenerateTokenAsync(user)
			};

			return response;
		}

		public async Task<UserDto> RegisterAsync(RegisterDto model)
		{
			var user = new ApplicationUser()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				UserName = model.UserName,
				PhoneNumber = model.Phone,
			};

			var result = await userManager.CreateAsync(user, model.Password);

			//if (!result.Succeeded)
			//	throw new ValidationException();

			var response = new UserDto()
			{
				Id = user.Id,
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await GenerateTokenAsync(user)
			};

			return response;

		}

		private async Task<string> GenerateTokenAsync(ApplicationUser user)
		{
			var userClaims = await userManager.GetClaimsAsync(user);
			var rolesAsClaims = new List<Claim>();

			var roles = await userManager.GetRolesAsync(user);


			foreach (var role in roles)
				rolesAsClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));

			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.PrimarySid,user.Id),
				new Claim(ClaimTypes.Email,user.Email!),
				new Claim(ClaimTypes.GivenName,user.DisplayName!),
			}
			.Union(userClaims)
			.Union(rolesAsClaims);

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-256-bit-secret"));
			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var tokenObj = new JwtSecurityToken(

				issuer: "TalabatIdentity",
				audience: "TalabatUsers",
				expires: DateTime.UtcNow.AddMinutes(10),
				claims: claims,
				signingCredentials: signingCredentials
				);


			return new JwtSecurityTokenHandler().WriteToken(tokenObj);
		}

		public async Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
		{
			var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
			var user = await userManager.FindByEmailAsync(email!);
			return new UserDto()
			{
				Id = user!.Id,
				Email = user!.Email!,
				DisplayName = user.DisplayName,
				Token = await GenerateTokenAsync(user),
			};
		}

		public async Task<AddressDto> GetUserAddress(ClaimsPrincipal claimsPrincipal)
		{

			var user = await userManager.FindUserWithAddress(claimsPrincipal!);
			var address = mapper.Map<AddressDto>(user!.Address);
			return address;
		}

	}
}
