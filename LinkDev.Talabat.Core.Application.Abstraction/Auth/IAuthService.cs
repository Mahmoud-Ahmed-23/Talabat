using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Talabat.Core.Application.Abstraction.Auth.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Common;

namespace LinkDev.Talabat.Core.Application.Abstraction.Auth
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);
		Task<AddressDto?> GetUserAddress(ClaimsPrincipal claimsPrincipal);
		Task<AddressDto> UpdateUserAddress(ClaimsPrincipal claimsPrincipal, AddressDto addressDto);
	}
}
