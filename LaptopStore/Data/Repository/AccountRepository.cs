using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using LaptopStore.Data.Enum;
using LaptopStore.Data.Helpers;
using LaptopStore.Data.Interfaces;
using LaptopStore.Data.Models;
using LaptopStore.Data.Response;
using LaptopStore.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LaptopStore.Data.Repository
{
    public class AccountRepository : IAccounts
    {
        private readonly IUsers _userRepository;
        private readonly IProfiles _profileRepository;
        private readonly ILogger<AccountRepository> _logger;
        
        public AccountRepository(IUsers userRepository, IProfiles profileRepository,
            ILogger<AccountRepository> logger)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.email == model.email);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "There is already a user with this email",
                    };
                }

                user = new User()
                {
                    email = model.email,
                    loginName = model.loginName,
                    role = Role.User,
                    password = HashPasswordHelper.HashPassword(model.password)
                };

                await _userRepository.Create(user);
                await _profileRepository.Create(new Profile { userId = user.id});
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Object added",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.email == model.email);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "User is not found"
                    };
                }

                if (user.password != HashPasswordHelper.HashPassword(model.password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Wrong Password"
                    };
                }
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}