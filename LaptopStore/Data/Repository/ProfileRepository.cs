using System;
using System.Linq;
using System.Threading.Tasks;
using LaptopStore.Data.Enum;
using LaptopStore.Data.Interfaces;
using LaptopStore.Data.Models;
using LaptopStore.Data.Response;
using LaptopStore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore.Data.Repository
{
    public class ProfileRepository : IProfiles
    {
        private readonly AppDBContent _db;

        public ProfileRepository(AppDBContent db)
        {
            _db = db;
        }

        

        public async Task<IBaseResponse<Profile>> GetProfile(string email)
        {
            try
            {
                var profile = await _db.Profiles
                    .Include(x => x.user)
                    .FirstOrDefaultAsync(x => x.user.email == email);
                if (profile == null)
                {
                    return new BaseResponse<Profile>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                return new BaseResponse<Profile>()
                {
                    StatusCode = StatusCode.OK,
                    Data = profile
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Profile>()
                {
                    Description = $"[Get] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task Create(Profile profile)
        {
            await _db.Profiles.AddAsync(profile);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Profile> GetAll()
        {
            return _db.Profiles;
        }

        public async Task Delete(Profile profile)
        {
            _db.Profiles.Remove(profile);
            await _db.SaveChangesAsync();
        }

        public async Task<Profile> Update(Profile profile)
        {
            _db.Profiles.Update(profile);
            await _db.SaveChangesAsync();

            return profile;
        }
    }
}