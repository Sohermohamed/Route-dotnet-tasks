using AutoMapper;
using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Exceptions;
using ECommerce.Persistence.Identity.Models;
using ECommerce.Shared.DTO_s.IdentityDto_s;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class AuthenticationServices(UserManager<ApplicationUser> userManager, IConfiguration configuration,IMapper mapper) : IAuthenticationServices
    {
        public async Task<UserDto> LoginAsync(LoginDto Dto)
        {
            var User = await userManager.FindByEmailAsync(Dto.Email) ?? throw new UserNotFoundException(Dto.Email);
            var IsPasswordValid = await userManager.CheckPasswordAsync(User, Dto.Password);

            if (IsPasswordValid)
            {
                return new UserDto
                {
                    Email = User.Email,
                    DisplayName = User.DisplayName,
                    Token = await CreateTokenAsync(User)
                };
            }
            else
            {
                throw new UnAutherizedException();
            }
        }

        public async Task<UserDto> RegisterAsync(RegisterDto Dto)
        {
            var user = new ApplicationUser()
            {
                DisplayName = Dto.DisplayName,
                Email = Dto.Email,
                UserName = Dto.UserName,
                PhoneNumber = Dto.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, Dto.Password);

            if (result.Succeeded)
            {
                return new UserDto
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = await CreateTokenAsync(user)
                };
            }
            else
            {
                var Errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto dto)
        {
            var User = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email) ?? throw new UserNotFoundException(email);
            if (User.Address is not null)
            {
                User.Address.Street = dto.Street;
                User.Address.City = dto.City;
                User.Address.Country = dto.Country;
                User.Address.FirstName = dto.FirstName;
                User.Address.LastName = dto.LastName;
            }
            else
            {
                User.Address = mapper.Map<AddressDto, Address>(dto);
            }
            await userManager.UpdateAsync(User);
            return mapper.Map<Address, AddressDto>(User.Address);
        }
        public async Task<bool> CheckEmailAsync(string email)
        {
            var User = await userManager.FindByEmailAsync(email);
            return User != null;
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var User = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email) ?? throw new UserNotFoundException(email);

            if (User is not null)
            {
                return mapper.Map<Address,AddressDto>(User.Address);
            }
            else
            {
                throw new AddressNotFoundExceptions(User.DisplayName);
            }
        }

        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var User = await userManager.FindByEmailAsync(email);

            return new UserDto
            {
                Email = User.Email,
                DisplayName = User.DisplayName,
                Token = await CreateTokenAsync(User)
            };
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.NameIdentifier, user.Id),
            };
            var Roles = await userManager.GetRolesAsync(user);

            foreach (var Role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, Role));
            }
            var SecurityKey = configuration.GetSection("JWTOptions")["SecurityKey"];

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Tokens = new JwtSecurityToken(
                issuer: configuration.GetSection("JWTOptions")["Issuer"],
                audience: configuration.GetSection("JWTOptions")["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: Creds
            );

            return new JwtSecurityTokenHandler().WriteToken(Tokens);
        }
    }
}
