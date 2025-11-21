using ECommerce.Abstraction.IServices;
using ECommerce.Shared.DTO_s.IdentityDto_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServicesManger servicesManger) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var User = await servicesManger.AuthenticationServices.LoginAsync(dto);
            return Ok(User);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            var User = await servicesManger.AuthenticationServices.RegisterAsync(dto);
            return Ok(User);
        }
        [Authorize]
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var IsEmailExist = await servicesManger.AuthenticationServices.CheckEmailAsync(email);
            return Ok(IsEmailExist);
        }
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser(string email)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var AppUser = await servicesManger.AuthenticationServices.GetCurrentUserAsync(Email);
            return Ok(AppUser);
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await servicesManger.AuthenticationServices.GetCurrentUserAddressAsync(Email);
            return Ok(Address);
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto dto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var UpdatedAddress = await servicesManger.AuthenticationServices.UpdateCurrentUserAddressAsync(Email, dto);
            return Ok(UpdatedAddress);
        }
    }
}
