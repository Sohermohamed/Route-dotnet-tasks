using ECommerce.Shared.DTO_s.IdentityDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction.IServices
{
    public interface IAuthenticationServices
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<bool> CheckEmailAsync(string email);
        Task<AddressDto> GetCurrentUserAddressAsync(string email);
        Task<AddressDto> UpdateCurrentUserAddressAsync(string email,AddressDto dto);
        Task<UserDto> GetCurrentUserAsync(string email);


    }
}
