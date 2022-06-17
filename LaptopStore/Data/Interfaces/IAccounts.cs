using System.Security.Claims;
using System.Threading.Tasks;
using LaptopStore.Data.Response;
using LaptopStore.ViewModels;

namespace LaptopStore.Data.Interfaces
{
    public interface IAccounts
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    }
}