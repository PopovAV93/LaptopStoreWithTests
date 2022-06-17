using System.Threading.Tasks;
using LaptopStore.Data.Models;
using LaptopStore.Data.Response;
using LaptopStore.ViewModels;

namespace LaptopStore.Data.Interfaces
{
    public interface IProfiles : IBaseRepository<Profile>
    {
        Task<IBaseResponse<Profile>> GetProfile(string email);
    }
}